using Grpc.Core;
using IdentitySample.Protos;
using IdentitySample.Server.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Server.Services
{
    [Authorize]
    public class AuthService : Auth.AuthBase
    {
        private readonly SignInManager<User> m_signInManager;
        private readonly UserManager<User> m_userManager;
        private readonly JwtSettings m_jwtSettings;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
            IOptions<JwtSettings> options)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
            m_jwtSettings = options.Value;
        }

        [AllowAnonymous]
        public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
        {
            var reply = new LoginReply();

            //return base.Login(request, context);
            var result = await m_signInManager.PasswordSignInAsync(
                request.Account, request.Password, false, false);

            if (!result.Succeeded)
            {
                reply.Success = false;
                reply.Message = "用户名或密码错误";
                return reply;
            }

            var user = await m_userManager.FindByNameAsync(request.Account);
            if (user == null)
            {
                reply.Success = false;
                reply.Message = "用户不存在";
            }

            reply.Success = true;
            reply.Token = await GenerateJwtToken(user);
            reply.ExpiresIn = 7200;
            reply.Account = user.UserName;
            reply.Name = user.NormalizedUserName ?? user.UserName;
            reply.Message = "登录成功";

            return reply;
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(m_jwtSettings.Secret);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await m_userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = m_jwtSettings.Issuer,
                Audience = m_jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
