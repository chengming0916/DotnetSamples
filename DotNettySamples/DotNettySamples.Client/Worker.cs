using DotNetty.Buffers;
using DotNetty.Common.Internal.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNettySamples.Client
{
    class Worker : BackgroundService
    {
        private readonly ILogger<Worker> m_logger;
        private readonly Config m_config;

        MultithreadEventLoopGroup m_group;
        Bootstrap m_bootstrap;
        ClientHandler m_clientHandler;
        private IChannel m_channel;

        public Worker(ILogger<Worker> logger, IOptions<Config> options)
        {
            m_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            m_config = options?.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            m_group = new MultithreadEventLoopGroup();
            m_bootstrap = new Bootstrap();
            m_clientHandler = new ClientHandler(this, cancellationToken);
            m_bootstrap.Group(m_group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.ConnectTimeout, TimeSpan.FromSeconds(5))
                .Handler(new ActionChannelInitializer<ISocketChannel>(x =>
                {
                    x.Pipeline.AddLast("timeout", new IdleStateHandler(TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5)));
                    x.Pipeline.AddLast(m_clientHandler);
                }));

            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await m_group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                m_channel = await m_bootstrap.ConnectAsync(m_config.Server.Host, m_config.Server.Port);
            }
            catch (Exception ex)
            {
                Thread.Sleep(5000);

                m_logger.LogError(ex, "连接失败，等待重连");

                await StartAsync(stoppingToken).ConfigureAwait(false);
            }
        }
    }

    class ClientHandler : ChannelHandlerAdapter
    {
        private readonly Worker m_worker;
        private readonly CancellationToken m_cancellationToken;
        private readonly ILogger<ClientHandler> m_logger;

        public ClientHandler(Worker worker, CancellationToken cancellationToken)
        {
            m_worker = worker;
            m_cancellationToken = cancellationToken;
            m_logger = InternalLoggerFactory.DefaultFactory.CreateLogger<ClientHandler>();
        }

        public override bool IsSharable => true;

        public override void HandlerAdded(IChannelHandlerContext context)
        {
            base.HandlerAdded(context);
            m_logger.LogInformation("handler added");
        }

        public override void HandlerRemoved(IChannelHandlerContext context)
        {
            base.HandlerRemoved(context);
            m_logger.LogInformation("handler removed");
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            base.ChannelRegistered(context);
            m_logger.LogInformation("channel registered");
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            base.ChannelUnregistered(context);
            m_logger.LogInformation("channel unregistered");
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            m_logger.LogInformation("channel actived");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
            m_logger.LogWarning("连接断开");
            m_worker.StartAsync(m_cancellationToken);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            //base.ChannelRead(context, message);
            if (message is IByteBuffer byteBuffer)
            {
                byte[] buffer = new byte[byteBuffer.ReadableBytes];
                byteBuffer.ReadBytes(buffer, 0, buffer.Length);

                m_logger.LogDebug($"msg: {Encoding.UTF8.GetString(buffer)}\n" +
                    $"remote: {context.Channel.RemoteAddress}\n");
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override async void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            await m_worker.StartAsync(m_cancellationToken);

            m_logger.LogError(exception.Message);
            await context.CloseAsync();
            //base.ExceptionCaught(context, exception);
        }

        public override async void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            if (evt is IdleStateEvent eventState)
            {
                switch (eventState.State)
                {
                    case IdleState.ReaderIdle:
                        break;
                    case IdleState.WriterIdle:
                        //TODO: write heart beat 
                        //await context.WriteAndFlushAsync();
                        break;
                    case IdleState.AllIdle:
                        break;
                    default:
                        break;
                }
            }

            base.UserEventTriggered(context, evt);
        }
    }
}
