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

            //设置程序运行主目录
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            var builder = Host.CreateDefaultBuilder(args);

            // 初始化日志
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

            // 配置服务
            builder.ConfigureServices((ctx, svc) =>
            {
                // 注册服务实例
                svc.AddHostedService<Worker>();

                // 加载配置文件
                svc.AddSingleton<Config>();
                svc.Configure<Config>(ctx.Configuration.GetSection("Config"));
            });

            var app = builder.Build();
            app.Run();
        }
    }
}
