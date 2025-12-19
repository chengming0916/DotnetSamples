using Grpc.Core;
using Grpc.Net.Client;
using IdentitySample.Protos;
using IdentitySample.Server;

namespace IdentitySample.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7048", new GrpcChannelOptions
                {
                    HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    }
                });
                var client = new Auth.AuthClient(channel);
                var greet = new Greeter.GreeterClient(channel);
                var reply = client.Login(new LoginRequest { Account = "admin", Password = "admin" });
                if (reply.Success)
                {
                    Console.WriteLine("Token: " + reply.Token);

                    var meatdata = new Metadata
                    {
                        {"authorization" , $"Bearer {reply.Token}"}
                    };

                    // 调用接口并传递Metadata
                    var r = greet.SayHello(new HelloRequest { Name = "TEST" }, meatdata);
                    Console.WriteLine(r.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
