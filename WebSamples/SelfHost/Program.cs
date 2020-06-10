using Microsoft.Owin.Hosting;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Unity;
using Unity;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IAppBuilder>();

            HostFactory.Run(cfg =>
            {
                cfg.UseUnityContainer(container);
                cfg.Service<ServiceRunner>();
                cfg.SetDisplayName("ASP.NET Web Api 自宿主示例");
                cfg.SetServiceName("SelfHostSample");
                cfg.SetDescription("ASP.NET Web Api 自宿主示例");
            });
        }
    }

    public class ServiceRunner : ServiceControl
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public bool Start(HostControl hostControl)
        {
            string baseAddress = "http://localhost:50001/";
            WebApp.Start<Startup>(baseAddress);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
