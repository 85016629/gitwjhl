using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace fx.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {

            

            var builder = new ConfigurationBuilder();
            //builder.SetBasePath(environment.ContentRootPath)
            //       .AddJsonFile("appsettings.json", false, reloadOnChange: true)
            //       .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
            //       .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
            //       .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceRegisterOptions>(Configuration.GetSection("ServiceRegister"));
            services.AddSingleton<IConsulClient>(p => new ConsulClient(
                cfg =>
                {
                    var serviceConfirguation = p.GetRequiredService<IOptions<ServiceRegisterOptions>>().Value;
                    if (string.IsNullOrEmpty(serviceConfirguation.Register.HttpEndpoint))
                    {
                        cfg.Address = new Uri(serviceConfirguation.Register.HttpEndpoint);
                    }
                }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOcelot(new ConfigurationBuilder()
                    .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                    .Build());

            #region Consul服务注册

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if(Configuration["ServiceRegister:IsActive"] == bool.TrueString)
            //{
            //    builder
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            await app.UseOcelot();

            app.UseMvc();
        }
    }
}
