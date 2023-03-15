using dotnet_etcd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KVSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new EtcdClient("tcp://127.0.0.1");

            var timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
            timer.Elapsed += (s, e) =>
            {
                client.Put("/EtcdSamples/KVSample/Now", DateTime.Now.ToString()); //写入数据
            };
            timer.Start();

            var timer1 = new Timer(); //每秒读取一次
            timer1.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            timer1.Elapsed += (s, e) =>
            {
                var result = client.GetVal("/EtcdSamples/KVSample/Now"); //读取数据
                Console.WriteLine(result);
            };
            timer1.Start();

            client.Put("/EtcdSamples/KVSample/Delete", "TEST");
            Console.WriteLine(client.GetVal("/EtcdSamples/KVSample/Delete"));

            Console.ReadKey();

            var response = client.Delete("/EtcdSamples/KVSample/Delete");
            if (response.Deleted)
            {

            }
            Console.WriteLine(client.GetVal("/EtcdSamples/KVSample/Delete"));

            Console.ReadKey();


        }
    }
}
