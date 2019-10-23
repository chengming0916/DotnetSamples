using Grpc.Core;
using GRPCDemo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static GRPCDemo.gRPC;

namespace GrpcSample.Server
{
    internal class Program
    {
        private const int port = 9007;

        private static void Main(string[] args)
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

    internal class GrpcImplement : gRPCBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
            //return base.SayHello(request, context);
        }

        public override Task ServerStreaming(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            string peer = context.Peer;
            return Task.Run(async () =>
            {
                while (true)
                {
                    await responseStream.WriteAsync(new HelloReply
                    {
                        Message = "server streaming receive:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
                    });
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            });
            //return base.ServerStreaming(request, responseStream, context);
        }

        public override Task<HelloReply> ClientStreaming(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            return Task.Run(async () =>
            {
                while (await requestStream.MoveNext())
                {
                    Console.WriteLine("client streaming request:" + requestStream.Current.Name);
                }
                return new HelloReply { Message = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") };
            });

            //return base.ClientStreaming(requestStream, context);
        }

        public override Task BidirectionalStreaming(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            return Task.Run(async () =>
            {
                while (await requestStream.MoveNext())
                {
                    var request = requestStream.Current;
                    Console.WriteLine(request.Name);
                    await responseStream.WriteAsync(new HelloReply
                    {
                        Message = "bidirect request:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
                    });
                }
            });

            //return base.BidirectionalStreaming(requestStream, responseStream, context);
        }

    }
}