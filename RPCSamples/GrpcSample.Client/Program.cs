using Grpc.Core;
using GRPCDemo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcSample.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:9007", ChannelCredentials.Insecure);

            var client = new gRPC.gRPCClient(channel);
            var reply = client.SayHello(new HelloRequest { Name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") });
            Console.WriteLine("来自" + reply.Message);

            //服务端流
            var reply1 = client.ServerStreaming(new HelloRequest
            {
                Name = "server streaming request:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
            });

            Task.Run(async () =>
            {
                while (await reply1.ResponseStream.MoveNext())
                {
                    var result = reply1.ResponseStream.Current;
                    Console.WriteLine(result.Message);
                }
            });

            //var reply2 = client.ClientStreaming();
            //Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        await reply2.RequestStream.WriteAsync(new HelloRequest
            //        {
            //            Name = "client streaming request:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
            //        });
            //        Thread.Sleep(TimeSpan.FromSeconds(2));
            //    }
            //    //关闭流
            //    //await reply2.RequestStream.CompleteAsync();
            //});

            //var reply3 = client.BidirectionalStreaming();
            //Task.Run(async () =>
            //{
            //    while (await reply3.ResponseStream.MoveNext())
            //    {
            //        var r = reply3.ResponseStream.Current;
            //        Console.WriteLine("receive:" + r.Message);
            //    }
            //});

            //Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        await reply3.RequestStream.WriteAsync(new HelloRequest
            //        {
            //            Name = "bidirect request:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
            //        });

            //        Thread.Sleep(TimeSpan.FromSeconds(3));
            //    }
            //});
            //await reply3.RequestStream.CompleteAsync();

            //channel.ShutdownAsync().Wait();
            Console.WriteLine("任意键退出...");
            Console.ReadKey();
        }
    }
}