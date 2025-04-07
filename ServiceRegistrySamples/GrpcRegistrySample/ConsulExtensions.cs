using Consul;

namespace GrpcRegistrySample
{
    public static class ConsulExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, ConsulConfig config)
        {
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var consulClient = new ConsulClient(cfg =>
            {
                cfg.Address = new Uri($"http://{config.Host}:{config.Port}");
                cfg.Datacenter = "dc1";
            });

            // Register the service with Consul
            consulClient.Agent.ServiceRegister(new AgentServiceRegistration()
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

            // Register application stopping event
            lifetime.ApplicationStopping.Register(async () =>
            {
                await consulClient.Agent.ServiceDeregister(config.ServiceId);
            });
            return app;
        }
    }
}
