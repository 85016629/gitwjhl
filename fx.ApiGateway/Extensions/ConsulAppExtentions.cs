using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.ApiGateway
{
    public static class ConsulAppExtentions
    {
        public static void ConsulApp(this IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IOptions<ServiceRegisterOptions> options, IConsulClient consul)
        {
            lifetime.ApplicationStarted.Register(() =>
            {
                RegisterService(app, lifetime, options, consul);
            });
        }

        private static void RegisterService(IApplicationBuilder app, IApplicationLifetime lifetime, IOptions<ServiceRegisterOptions> options, IConsulClient consul)
        {
            var feature = app.Properties["server.Features"] as FeatureCollection;

            var addressList = feature.Get<IServerAddressesFeature>()
                .Addresses
                .Select(p => new Uri(p));

            foreach (var address in addressList)
            {
                var serviceId = $"{options.Value.ServiceName}_{ address.Host} : {address.Port}";
                var httpCheck = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                    Interval = TimeSpan.FromSeconds(30),
                    HTTP = new Uri(address, "HealCheck").OriginalString
                };

                var registration = new AgentServiceRegistration
                {
                    Checks = new[] { httpCheck },
                    Address = address.Host,
                    ID = serviceId,
                    Name = options.Value.ServiceName,
                    Port = address.Port
                };

                consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

                lifetime.ApplicationStopped.Register(() =>
                {
                    consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
                });
            }
        }
    }
}
