using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thrift.Protocol;
using Thrift.Transport;

namespace ThriftSample.Client
{
    class Program
    {
        static Dictionary<String, String> map = new Dictionary<String, String>();
        static List<Blog> blogs = new List<Blog>();

        static void Main(string[] args)
        {
            TTransport transport = new TSocket("localhost", 7911);
            TProtocol protocol = new TBinaryProtocol(transport);
            ThriftCase.Client client = new ThriftCase.Client(protocol);
            transport.Open();
            Console.WriteLine("Client calls .....");
            map.Add("blog", "http://www.javabloger.com");

            client.testCase1(10, 21, "3");
            client.testCase2(map);
            client.testCase3();

            Blog blog = new Blog
            {
                //blog.setContent("this is blog content".getBytes()); 
                CreatedTime = DateTime.Now.Ticks,
                Id = "123456",
                IpAddress = "127.0.0.1",
                Topic = "this is blog topic"
            };
            blogs.Add(blog);

            client.testCase4(blogs);

            transport.Close();

            Console.ReadKey();
        }
    }
}
