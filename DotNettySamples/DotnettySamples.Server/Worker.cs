using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNettySamples.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnettySamples.Server.Net5
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> m_logger;
        private readonly IConfiguration m_configuration;
        private IEventLoopGroup m_bossGroup = new MultithreadEventLoopGroup(1);
        private IEventLoopGroup m_udpWorkderGroup = new MultithreadEventLoopGroup();
        private IEventLoopGroup m_tcpWorkderGroup = new MultithreadEventLoopGroup();
        private IChannel m_udpChannel;
        private IChannel m_tcpChannel;
        int m_port;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            m_logger = logger;
            m_configuration = configuration;
            m_port = m_configuration.GetValue<int>("Port");
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        m_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //        await Task.Delay(1000, stoppingToken);
        //    }
        //}

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await m_tcpChannel.CloseAsync();
            await m_udpChannel.CloseAsync();
            await Task.WhenAll(
                m_udpWorkderGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                m_tcpWorkderGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                m_bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1))
                );

            //return base.StopAsync(cancellationToken);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        private async Task RunUpdServer()
        {
            var udpBootstrap = new Bootstrap();

            udpBootstrap.Group(m_udpWorkderGroup)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Option(ChannelOption.SoReuseaddr, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    channel.Pipeline.AddLast("UDP", new UdpServerHandler());
                }));

            m_udpChannel = await udpBootstrap.BindAsync(m_port);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await RunUpdServer();

            await RunTcpServer();
        }

        private async Task RunTcpServer()
        {
            var tcpBootstrap = new ServerBootstrap();
            tcpBootstrap.Group(m_bossGroup, m_tcpWorkderGroup);
            tcpBootstrap.Channel<TcpServerSocketChannel>()
                .ChildOption(ChannelOption.SoKeepalive, true);
            tcpBootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast("TCP", new TcpServerHandler());
            }));

            m_tcpChannel = await tcpBootstrap.BindAsync(m_port);
        }
    }
}