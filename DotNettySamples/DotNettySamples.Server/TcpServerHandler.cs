using DotNetty.Buffers;
using DotNetty.Common.Internal.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNettySamples.Server
{
    public class TcpServerHandler : ChannelHandlerAdapter
    {
        private readonly ILogger<TcpServerHandler> m_logger;

        public TcpServerHandler()
        {
            m_logger = InternalLoggerFactory.DefaultFactory.CreateLogger<TcpServerHandler>();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer buffer)
                m_logger.LogInformation("Server recevied tcp message ：" + buffer.ToString(Encoding.UTF8));

            Thread.Sleep(TimeSpan.FromSeconds(10));
            byte[] bytes = Encoding.UTF8.GetBytes("Hello client " + DateTime.Now.Ticks);
            IByteBuffer byteBuffer = Unpooled.WrappedBuffer(bytes);
            context.WriteAndFlushAsync(byteBuffer);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            m_logger.LogDebug("channel active");
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            m_logger.LogDebug("channel inactive");
            base.ChannelInactive(context);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            m_logger.LogInformation(exception.GetType().ToString());
            //base.ExceptionCaught(context, exception);
            if (exception is ReadTimeoutException)
                m_logger.LogWarning("超时关闭连接,ReadTimeout");
            else if (exception is WriteTimeoutException)
                m_logger.LogWarning("超时关闭连接,WriteTimeout");
            else
                m_logger.LogError(exception, "");
            context.CloseAsync();
        }

        public override async void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            m_logger.LogDebug("User event triggered");
            //base.UserEventTriggered(context, evt);
            if (evt is IdleStateEvent stateEvent)
            {
                switch (stateEvent.State)
                {
                    case IdleState.ReaderIdle:
                        m_logger.LogDebug("ReaderIdle");
                        break;
                    case IdleState.WriterIdle:
                        m_logger.LogDebug("WriterIdle");
                        break;
                    case IdleState.AllIdle:
                        m_logger.LogDebug("AllIdle");
                        //await context.CloseAsync(); // Idle 5秒后断开连接
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
