﻿using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNettySamples.Server
{
    class Program
    {
        static void Main(string[] args) => RunServerAsync().Wait();

        static async Task RunServerAsync()
        {
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            IEventLoopGroup udpWorkerGroup = new MultithreadEventLoopGroup();
            IEventLoopGroup tcpWorkerGroup = new MultithreadEventLoopGroup();
            var udpBootstrap = new Bootstrap();
            var tcpBootstrap = new ServerBootstrap();
            try
            {
                udpBootstrap.Group(udpWorkerGroup)
                    .Channel<SocketDatagramChannel>()
                    .Option(ChannelOption.SoBroadcast, true)
                    .Option(ChannelOption.SoReuseaddr, true)
                    .Handler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        channel.Pipeline.AddLast("UDP", new UdpServerHandler());
                    }));

                tcpBootstrap.Group(bossGroup, tcpWorkerGroup);
                tcpBootstrap.Channel<TcpServerSocketChannel>()
                    .ChildOption(ChannelOption.SoKeepalive, true);
                tcpBootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast("TCP", new TcpServerHandler());
                    //pipeline.AddLast(new NumberEncoder(), new BigIntegerDecoder(), new FactorialServerHandler());
                }));

                IChannel tcpChannel = await tcpBootstrap.BindAsync(8002);
                IChannel udpChannel = await udpBootstrap.BindAsync(8002);
                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                await tcpChannel.CloseAsync();
                await udpChannel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await Task.WhenAll(
                 bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                 tcpWorkerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                 udpWorkerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1))
                );
            }
        }
    }
}
