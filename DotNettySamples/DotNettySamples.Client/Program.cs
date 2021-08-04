using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNettySamples.Client
{
    class Program
    {
        static async Task RunClientAsync()
        {
            var eventLoopGroup = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();
            //bootstrap.Group(eventLoopGroup).Channel<TcpSocketChannel>()
            //    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
            //    {
            //        IChannelPipeline pipeline = channel.Pipeline;
            //        pipeline.AddLast(new EchoClientHandler());
            //    }));

            bootstrap.Group(eventLoopGroup)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Option(ChannelOption.SoReuseaddr, true).
                Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    channel.Pipeline.AddLast("Udp", new UdpClientHandler());
                }));


            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
            //IChannel clientChannel = await bootstrap.ConnectAsync(remoteEndPoint);
            IChannel clientChannel = await bootstrap.BindAsync(IPEndPoint.MinPort);
            try
            {
                ThreadPool.QueueUserWorkItem(callback =>
                {
                    while (true)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes("Hello server" + DateTime.Now.Ticks);
                        IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
                        clientChannel.WriteAndFlushAsync(new DatagramPacket(buffer, remoteEndPoint));
                        Thread.Sleep(200);
                    }
                });

                while (true)
                {

                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        break;
                }
            }
            finally
            {
                await clientChannel.CloseAsync();
                await eventLoopGroup.ShutdownGracefullyAsync();
            }

        }

        static void Main(string[] args) => RunClientAsync().Wait();
    }
}
