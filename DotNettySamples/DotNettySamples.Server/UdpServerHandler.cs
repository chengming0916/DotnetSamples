using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNettySamples.Server
{
    class UdpServerHandler : SimpleChannelInboundHandler<DatagramPacket>
    {
        public override bool IsSharable => true;

        protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket msg)
        {
            if (!msg.Content.IsReadable()) return;

            string message = msg.Content.ToString(Encoding.UTF8);

            Console.WriteLine($"Server received udp message => {message}");

            byte[] bytes = Encoding.UTF8.GetBytes("Hello client" + DateTime.Now.Ticks);

            IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);

            ctx.WriteAndFlushAsync(new DatagramPacket(buffer, msg.Sender));

            //ctx.CloseAsync();
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);

            Console.WriteLine($"Exception: {exception}");
            context.CloseAsync();
        }
    }
}
