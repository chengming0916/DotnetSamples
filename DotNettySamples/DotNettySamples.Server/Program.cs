using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
using System.Text;
using System.IO;
using DotNetty.Common.Internal.Logging;
using System.Runtime.InteropServices;

namespace DotnettySamples.Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //���ó���������Ŀ¼
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            var builder = Host.CreateDefaultBuilder(args);

            // ��ʼ����־
            InternalLoggerFactory.DefaultFactory = LoggerFactory.Create(cfg => cfg.AddNLog());

            builder.ConfigureLogging(cfg =>
            {
                cfg.ClearProviders();
                cfg.AddNLog();
            });

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //    builder.UseWindowsService();
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //    builder.UseSystemd();

            // ���÷���
            builder.ConfigureServices((ctx, svc) =>
            {
                // ע�����ʵ��
                svc.AddHostedService<Worker>();

                // ���������ļ�
                svc.AddSingleton<Config>();
                svc.Configure<Config>(ctx.Configuration.GetSection("Config"));
            });

            var app = builder.Build();
            app.Run();
        }
    }
}
