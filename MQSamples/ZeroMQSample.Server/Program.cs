using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroMQSample.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var responseSocket = new ResponseSocket("tcp://*:5560"))
            //{
            //    while (true)
            //    {
            //        var message = responseSocket.ReceiveFrameString();
            //        Console.WriteLine("server received {0}", message);
            //        Console.WriteLine("server send world");
            //        responseSocket.SendFrame("world");
            //    }
            //}

            using (var publisher = new PublisherSocket())
            {
                publisher.Bind("tcp://127.0.0.1:5556");
                while (true)
                {
                    publisher.SendMoreFrame("TestTopic")
                        .SendFrame(DateTime.Now.ToLongTimeString());

                    Thread.Sleep(200);
                }
            }
        }
    }
}
