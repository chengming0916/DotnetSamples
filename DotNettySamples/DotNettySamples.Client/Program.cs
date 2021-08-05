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
        static async Task RunTcpClientAsync()
        {
            var eventLoopGroup = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();
            try
            {
                bootstrap.Group(eventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast("TCP", new TcpClientHandler());
                }));

                var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
                IChannel clientChannel = await bootstrap.ConnectAsync(remoteEndPoint);

                ThreadPool.QueueUserWorkItem(callback =>
                {
                    while (true)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes("Hello server" + DateTime.Now.Ticks);
                        IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
                        clientChannel.WriteAndFlushAsync(buffer);
                        Thread.Sleep(200);
                    }
                });

                //while (true)
                //{
                //    if (Console.ReadKey().Key == ConsoleKey.Escape)
                //        break;
                //}

                //await clientChannel.CloseAsync();
            }
            finally
            {
                //await clientChannel.CloseAsync();
                //await eventLoopGroup.ShutdownGracefullyAsync();
            }
        }

        static async Task RunUdpClientAsync()
        {
            var eventLoopGroup = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();

            bootstrap.Group(eventLoopGroup)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Option(ChannelOption.SoReuseaddr, true).
                Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    channel.Pipeline.AddLast("UDP", new UdpClientHandler());
                }));


            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
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

                //while (true)
                //{
                //    if (Console.ReadKey().Key == ConsoleKey.Escape)
                //        break;
                //}
                //await clientChannel.CloseAsync();
            }
            finally
            {
                //await clientChannel.CloseAsync();
                //await eventLoopGroup.ShutdownGracefullyAsync();
            }
        }

        static void Main(string[] args)
        {
            Task.Run(RunUdpClientAsync);
            Task.Run(RunTcpClientAsync);

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
            }
        }
    }
}
