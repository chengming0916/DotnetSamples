using DotNetty.Handlers.Logging;
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
        static async Task Main(string[] args)
        {
            IEventLoopGroup eventLoopGroup = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();
            try
            {
                bootstrap.Group(eventLoopGroup)
                    .Channel<SocketDatagramChannel>()
                    .Option(ChannelOption.SoBroadcast, true)
                    .Option(ChannelOption.SoReuseaddr, true)
                    .Handler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        channel.Pipeline.AddLast("Udp", new UdpServerHandler());
                    }));

                //bootstrap.Group(eventLoopGroup);
                //bootstrap.Channel<TcpServerSocketChannel>()
                //    .ChildOption(ChannelOption.SoKeepalive, true);
                //bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                //{
                //    IChannelPipeline pipeline = channel.Pipeline;
                //    pipeline.AddLast(new EchoServerHandler());
                //}));

                //bootstrap.Channel<SocketDatagramChannel>()
                //    .Option(ChannelOption.SoBroadcast, true)
                //    .Option(ChannelOption.SoReuseaddr, true);
                //bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
                //{
                //    IChannelPipeline pipeline = channel.Pipeline;
                //    pipeline.AddLast(new LoggingHandler());
                //    pipeline.AddLast(new UdpServerHandler());
                //}));

                IChannel boundChannel = await bootstrap.BindAsync(8002);
                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await eventLoopGroup.ShutdownGracefullyAsync();
            }
        }
    }
}
