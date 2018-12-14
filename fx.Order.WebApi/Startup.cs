using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fx.Domain.Bus;
using fx.Domain.core;
using fx.Domain.OrderContext;
using fx.Infra.Data.SqlServer;
using fx.Order.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace fx.Order.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddScoped<IMemoryBus, MediatBus>();
            services.AddScoped<IEventStore<DomainEvent>, EventStore>();

            services.AddMediatR(typeof(Startup));
            //typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) }
            //services.AddScoped<IRequestHandler<UpdateLastLoginTimeCommand>, CustomerCommandExecutor>();
            services.AddScoped(typeof(IRequestHandler<CreateOrderCommand, object>), typeof(OrderCommandExecutor));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("OrderService", new Info
                {
                    Version = "v1",
                    Title = "订单服务 API"
                });

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "fx.Order.WebApi.xml");
                options.IncludeXmlComments(xmlPath);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseStaticFiles();

            loggerFactory.AddNLog();
            app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("OrderService/swagger.json", "Order API V1");
            });            

            app.RegisterConsul(lifetime, new ServiceEntity
            {
                ConsulIP = Configuration["Consul:IP"],
                ConsulPort = int.Parse(Configuration["Consul:Port"]),
                IP = Configuration["Service:IP"],
                Port = int.Parse(Configuration["Service:Port"]),
                ServiceName = Configuration["Service:Name"]
            });

            app.UseMvc();
        }
    }
}
