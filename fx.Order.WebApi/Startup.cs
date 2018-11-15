using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fx.Domain.Bus;
using fx.Domain.core;
using fx.Domain.OrderContext;
using fx.Infra.Data.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
