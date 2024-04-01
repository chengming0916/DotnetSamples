using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PInvoke
{
    internal class Program
    {
        private const string DLL_IMPORT = "TEST.dll";

        [DllImport(DLL_IMPORT, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TEST(IntPtr handle);

        static void Main(string[] args)
        {
            //拼装DLL路径
            var dllFile = Path.Combine(Environment.Is64BitProcess ? "x64" : "x86", DLL_IMPORT);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dllFile = Path.Combine(path, dllFile);
            //加载DLL
            if (File.Exists(dllFile)) Assembly.LoadFile(dllFile);
        }
    }
}
