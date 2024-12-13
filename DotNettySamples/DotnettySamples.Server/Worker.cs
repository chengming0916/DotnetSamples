using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNettySamples.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnettySamples.Server
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> m_logger;
        private readonly Config m_config;
        private IEventLoopGroup m_bossGroup = new MultithreadEventLoopGroup(1);
        private IEventLoopGroup m_udpWorkderGroup = new MultithreadEventLoopGroup();
        private IEventLoopGroup m_tcpWorkderGroup = new MultithreadEventLoopGroup();
        private IChannel m_udpChannel;
        private IChannel m_tcpChannel;

        public Worker(ILogger<Worker> logger, IOptions<Config> options)
        {
            m_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            m_config = options.Value;

        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            //await RunUpdServer();

            await RunTcpServer();
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
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // 程序升级

                // 节点登录/注册

                // 定时心跳

                // 看门狗

                // 配置更新

                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
        }

        private async Task RunTcpServer()
        {
            var tcpBootstrap = new ServerBootstrap();
            tcpBootstrap.Group(m_bossGroup, m_tcpWorkderGroup);
            tcpBootstrap.Channel<TcpServerSocketChannel>()
                .ChildOption(ChannelOption.SoKeepalive, true);
            tcpBootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                //channel.Pipeline.AddLast("timeout", new IdleStateHandler(5, 5, 5)); // 连接空闲时间太长时触发UserEventTrigger
                channel.Pipeline.AddLast("read-timeout", new ReadTimeoutHandler(50));  // 读空闲太长时间触发异常
                channel.Pipeline.AddLast("write-timeout", new WriteTimeoutHandler(5));// 写空闲太长时间触发异常
                channel.Pipeline.AddLast("TCP", new TcpServerHandler());
            }));

            m_tcpChannel = await tcpBootstrap.BindAsync(m_config.Port);
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

            m_udpChannel = await udpBootstrap.BindAsync(m_config.Port);
        }
    }
}