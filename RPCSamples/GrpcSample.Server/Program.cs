using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GRPCDemo;
using static GRPCDemo.gRPC;

namespace GrpcSample.Server
{
    class Program
    {
        const int port = 9007;
        static void Main(string[] args)
        {
            Grpc.Core.Server server = new Grpc.Core.Server
            {
                Services = { gRPC.BindService(new GrpcImplement()) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("gRPC server listening on port " + port);
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }

    class GrpcImplement : gRPCBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
            //return base.SayHello(request, context);
        }
    }
}
