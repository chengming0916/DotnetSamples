using IdentitySample.Server.Domain;
using IdentitySample.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentitySample.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS,
            // visit https://go.microsoft.com/fwlink/?linkid=2099682

            // DbContext配置
            builder.Services.AddDbContext<DataContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ASP.NET Identity配置
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero,
                };

                // 关键： 让 JWT 认证支持 gRPC 的 Metadata (gRPC 不使用常规 HTTP 请求头)
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // 从 gRPC Metadata 中读取 Token (Key 为 "authorization")
                        var metadata = context.HttpContext.Request.Headers["authorization"];
                        if (!string.IsNullOrEmpty(metadata))
                        {
                            // 移除 "Bearer" 前缀（gRPC 客户端传递时需带前缀）
                            context.Token = metadata.ToString().Replace("Bearer ", "");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            // 5. 添加授权策略（可选：基于角色/声明的授权）
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddSingleton(jwtSettings);

            var app = builder.Build();

            // 中间件配置
            app.UseAuthentication(); // 先认证
            app.UseAuthorization();  // 后授权

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<AuthService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. " +
            "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}