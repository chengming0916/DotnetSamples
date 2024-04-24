using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
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
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer buffer)
                Console.WriteLine("Server recevied tcp message ：" + buffer.ToString(Encoding.UTF8));

            byte[] bytes = Encoding.UTF8.GetBytes("Hello client " + DateTime.Now.Ticks);
            IByteBuffer byteBuffer = Unpooled.WrappedBuffer(bytes);
            context.WriteAndFlushAsync(byteBuffer);
        }

        //public override void ChannelActive(IChannelHandlerContext context)
        //{
        //    //base.ChannelActive(context);

        //    byte[] bytes = Encoding.UTF8.GetBytes("Hello client " + DateTime.Now.Ticks);

        //    IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);

        //    context.WriteAsync(buffer);
        //}

		// 消息读完一定要清空，否则会导致粘包
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.CloseAsync();
            //base.ExceptionCaught(context, exception);
        }
    }
}
