using Grpc.Core;
using Grpc.Net.Client;
using ID4Samples.Server.Grpc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ID4Samples.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var content = new StringContent("grant_type=client_credentials&client_id=client&client_secret=secret",
                System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = httpClient.PostAsync("https://localhost:7131/connect/token", content).Result;

            if (!response.IsSuccessStatusCode)
            {

                Console.WriteLine($"Error: {response.StatusCode}");
                return;
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var token = JsonDocument.Parse(json).RootElement.GetProperty("access_token").GetString();
            Console.WriteLine($"token: {token}");

            //var metadata = new Metadata
            //{
            //    {"Authorization", $"Bearer {token}" }
            //};

            //var channel = GrpcChannel.ForAddress("https://localhost:7178");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = client.SayHello(new HelloRequest
            //{
            //    Name = "World"
            //}, new CallOptions(metadata));

            //Console.WriteLine($"reply message: {reply.Message}");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = httpClient.GetAsync("https://localhost:7083/WeatherForecast").Result;
            if (result.IsSuccessStatusCode)
            {
                json = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
            }
        }
    }
}
