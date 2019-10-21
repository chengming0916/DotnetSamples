using System;
using System.Messaging;
using System.Timers;

namespace MSMQSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //消息队列路径
            string path = ".\\Private$\\myQueue";

            MessageQueue messageQueue;
            if (!MessageQueue.Exists(path))
            {
                messageQueue = MessageQueue.Create(path);
            }
            else
            {
                messageQueue = new MessageQueue(path);
            }
            Timer timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(2).TotalMilliseconds;
            timer.Elapsed += (s, e) =>
            {
                Message message = new Message()
                {
                    Body = "Hello",
                    Formatter = new XmlMessageFormatter(new Type[] { typeof(string) })
                };
                Console.WriteLine("发送消息--->" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                messageQueue.Send(message);
            };
            timer.Start();
            Console.ReadKey();
        }
    }
}