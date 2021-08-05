using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNettySamples.Client
{
    public class TcpClientHandler : ChannelHandlerAdapter
    {
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer buffer)
                Console.WriteLine("Client received tcp message : " + buffer.ToString(Encoding.UTF8));

            //byte[] bytes = Encoding.UTF8.GetBytes("Hello server " + DateTime.Now.Ticks);
            //IByteBuffer byteBuffer = Unpooled.WrappedBuffer(bytes);
            //context.WriteAndFlushAsync(byteBuffer);
        }

        //public override void ChannelActive(IChannelHandlerContext context)
        //{
        //    byte[] bytes = Encoding.UTF8.GetBytes("Hello server " + DateTime.Now.Ticks);
        //    IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
        //    context.WriteAndFlushAsync(buffer);
        //}

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);
            Console.WriteLine(exception);
            context.CloseAsync();
        }
    }
}
