using DotNetty.Buffers;
using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
using System.Runtime.InteropServices;

namespace DotNettySamples.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 设置程序运行主目录
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            var builder = Host.CreateDefaultBuilder(args);

            //Initialize logger
            var loggingBuilder = new Action<ILoggingBuilder>(cfg =>
            {
                cfg.ClearProviders();
                cfg.AddNLog();
            });
            InternalLoggerFactory.DefaultFactory = LoggerFactory.Create(loggingBuilder);
            builder.ConfigureLogging(loggingBuilder);

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //    builder.UseWindowsService();
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //    builder.UseSystemd();

            builder.ConfigureServices((ctx, svc) =>
            {
                svc.AddHostedService<Worker>();

                svc.AddSingleton<Config>();
                svc.Configure<Config>(ctx.Configuration.GetSection("Config"));
            });

            var app = builder.Build();

            app.Run();
        }
    }
}