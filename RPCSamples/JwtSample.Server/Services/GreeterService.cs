using Grpc.Core;
using JwtSample.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace JwtSample.Server.Services
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> m_logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            m_logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream,
            ServerCallContext context)
        {
            //return base.SayHelloStream(request, responseStream, context);
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "server streaming receive:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
                });

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        public override async Task<HelloReply> StreamSayHello(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            //return base.StreamSayHello(requestStream, context);
            while (await requestStream.MoveNext())
            {
                Console.WriteLine("client streaming request:" + requestStream.Current.Name);
            }

            return new HelloReply { Message = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") };
        }

        public override async Task BidirectionalSayHello(IAsyncStreamReader<HelloRequest> requestStream,
            IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            //return base.BidirectionalSayHello(requestStream, responseStream, context);
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                Console.WriteLine(request.Name);

                var reply = new HelloReply
                {
                    Message = "bidirect request:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
                };
                await responseStream.WriteAsync(reply);
            }
        }

        //[AllowAnonymous]
        public override async Task<GetTokenReply> GetToken(GetTokenRequest request, ServerCallContext context)
        {
            //return base.GetToken(request, context);
            var reply = new GetTokenReply();

            if (request.Account == "admin" && request.Password == "admin")
            {
                reply.Token = GenerateJwtToken(request.Account);
                //reply.Expire = 60;
                return await Task.FromResult(reply);
            }

            return await Task.FromResult(reply);
        }

        private string GenerateJwtToken(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Name is not specified.");
            //var claims = new[] { new Claim(ClaimTypes.Name, name) };
            //var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            //var token = new JwtSecurityToken("JwtSample.Server", "JwtSample", claims,
            //    expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);

            return string.Empty;
        }
    }
}