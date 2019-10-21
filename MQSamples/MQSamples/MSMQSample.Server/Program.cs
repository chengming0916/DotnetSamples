using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQSample.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //消息队列路径
            string path = ".\\Private$\\myQueue";
            MessageQueue messageQueue;
            if (MessageQueue.Exists(path))
            {
                messageQueue = new MessageQueue(path);
            }
            else
            {
                messageQueue = MessageQueue.Create(path);
            }
            Task.Run(() =>
            {
                while (true)
                {
                    Message message = messageQueue.Receive();//消息为阻塞模式
                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    string str = message.Body.ToString();
                    Console.WriteLine("接收消息--->" + str + "--->" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                }
            });

            Console.ReadKey();
        }
    }
}
