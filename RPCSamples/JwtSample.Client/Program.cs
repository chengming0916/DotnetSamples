using Grpc.Core;
using Grpc.Net.Client;

namespace JwtSample.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7008");
            var client = new JwtSample.Greeter.GreeterClient(channel);
            var reply = client.GetToken(new GetTokenRequest() { Account = "admin", Password = "admin" });
            Console.WriteLine($"Token: {reply.Token}");

            Console.ReadKey();
        }
    }
}
