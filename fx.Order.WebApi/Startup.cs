using System.IO;
using fx.Application.Order.Interfaces;
using fx.Application.Order.Services;
using fx.Domain.Bus;
using fx.Domain.core;
using fx.Domain.OrderContext;
using fx.Infra.Data.SqlServer;
using fx.Order.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace fx.Order.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            #region 注入Cap.RabbitMQ

            services.AddDbContext<CapDbContext>(options=> {
                //options.UseSqlServer("Server=localhost;database=capmsg;userid=admin;password=111111");
                options.UseSqlServer(@"Server=.;Database=capmsg;Trusted_Connection=True;");
            });

            services.AddCap(x =>
            {
                x.UseEntityFramework<CapDbContext>();
                x.UseDashboard();
                x.UseRabbitMQ(mq =>
                {
                    mq.HostName = "localhost";
                    mq.Password = "guest";
                    mq.Port = 5672;
                    mq.ExchangeName = "cap.text.exchangeMsg";
                });
                //消息保持多长时间，会根据这个定期的清理数据
                x.SucceedMessageExpiredAfter = 24 * 3600;
                //设置失败以后重试的次数
                x.FailedRetryInterval = 5;
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="lifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            loggerFactory.AddNLog();
#pragma warning disable CS0618 // 类型或成员已过时
            app.AddNLogWeb();
#pragma warning restore CS0618 // 类型或成员已过时
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
