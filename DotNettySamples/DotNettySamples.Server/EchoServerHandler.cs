using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNettySamples.Server
{
    public class EchoServerHandler : ChannelHandlerAdapter
    {
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            //base.ChannelRead(context, message);
            IByteBuffer msg = message as IByteBuffer;
            Console.WriteLine("收到消息：" + msg.ToString(Encoding.UTF8));
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
            //base.ChannelReadComplete(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.CloseAsync();
            //base.ExceptionCaught(context, exception);
        }
    }
}
