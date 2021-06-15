using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace ZeroMQSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var requestSocket = new RequestSocket("tcp://localhost:5560"))
            //{
            //    while (true)
            //    {
            //        Console.WriteLine("client send hello");
            //        requestSocket.SendFrame("hello");

            //        var message = requestSocket.ReceiveFrameString();
            //        Console.WriteLine("requestSocket : Received '{0}'", message);

            //        Thread.Sleep(2000);
            //    }

            //    //Console.ReadKey();
            //}

            using (var subscriber = new SubscriberSocket())
            {
                subscriber.Connect("tcp://127.0.0.1:5556");
                subscriber.Subscribe("TestTopic");

                while (true)
                {
                    var topic = subscriber.ReceiveFrameString();
                    var msg = subscriber.ReceiveFrameString();
                    Console.WriteLine("Frame Publisher:{0} {1}", topic, msg);
                }
            }


        }
    }
}
