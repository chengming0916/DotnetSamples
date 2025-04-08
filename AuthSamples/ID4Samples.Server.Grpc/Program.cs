using ID4Samples.Server.Grpc.Services;
using IdentityServer4.AccessTokenValidation;

namespace ID4Samples.Server.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS,
        // visit https://go.microsoft.com/fwlink/?linkid=2099682

        // Add services to the container.
        builder.Services.AddGrpc();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:7131"; // IdentityServer�ĵ�ַ
                options.RequireHttpsMetadata = false; // �Ƿ���ҪHTTPS
                //options.ApiName = "api1"; // ��Ҫ��֤��API����
                //options.ApiSecret = "secret"; // API��Կ
            });

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<GreeterService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. " +
        "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}