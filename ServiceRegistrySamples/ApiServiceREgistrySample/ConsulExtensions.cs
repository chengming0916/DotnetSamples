using Consul;
using System.Threading.Tasks;

namespace ApiServiceREgistrySample
{
    public static class ConsulExtensions
    {
        public static async Task UseConsul(this IApplicationBuilder app, ConsulConfig config)
        {
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var consulClient = new ConsulClient(cfg =>
            {
                cfg.Address = new Uri($"http://{config.Host}:{config.Port}");
                cfg.Datacenter = "dc1";
            });

            await consulClient.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = config.ServiceId,
                Name = config.ServiceName,
                Address = config.ServiceHost,
                Port = config.ServicePort,
                Tags = new[] { "Consul Example Service" },
                Check = new AgentServiceCheck()
                {
                    HTTP = $"http://{config.ServiceHost}:{config.ServicePort}/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30),
                }
            });
        }
    }
}
