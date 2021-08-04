using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Text;

namespace DotNettySamples.Client
{
    public class UdpClientHandler : SimpleChannelInboundHandler<DatagramPacket>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket msg)
        {
            if (!msg.Content.IsReadable()) return;

            string message = msg.Content.ToString(Encoding.UTF8);

            Console.WriteLine($"Client Receive => {message}");

            //ctx.CloseAsync();
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            //base.ChannelReadComplete(context);
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}