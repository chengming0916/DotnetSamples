using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostics2File
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("this is a test for system.diagnostics", "Trace");
            Debug.WriteLine("this is a test for system.diagnostics", "Debug");
        }
    }
}
