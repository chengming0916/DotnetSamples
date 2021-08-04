using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNettySamples.Client
{
    public class EchoClientHandler : SimpleChannelInboundHandler<IByteBuffer>
    {
        public static int i = 0;

        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBuffer msg)
        {
            if (msg != null)
            {
                i++;
                Console.WriteLine($"Receive from server {i}:" + msg.ToString(Encoding.UTF8));
            }

            ctx.WriteAsync(Unpooled.CopiedBuffer(msg));
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
            //base.ChannelReadComplete(context);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            //base.ChannelActive(context);
            Console.WriteLine($"发送客户端消息");
            context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes($"客户端消息!")));
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);
            Console.WriteLine(exception);
            context.CloseAsync();
        }
    }
}
