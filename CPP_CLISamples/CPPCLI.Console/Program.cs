using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPCLI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int left = 10, right = 20;

            InvokeCon invokeCon = new InvokeCon();
            var result = invokeCon.SumCli(left, right);

            System.Console.WriteLine("调用C++ Sum Result->" + result);

            result = invokeCon.SubCli(left, right);

            System.Console.WriteLine("调用C++ Sub Result->" + result);

            result = invokeCon.MultCli(left, right);

            System.Console.WriteLine("调用C++ Mult Result->" + result);

            result = invokeCon.DividedCli(left, right);

            System.Console.WriteLine("调用C++ Divided Result->" + result);

            System.Console.ReadKey();
        }
    }
}
