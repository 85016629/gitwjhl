using System;
using System.IO;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using Ocelot.Provider.Consul;
using fx.ApiGateway.Extensions;

namespace fx.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// 系统配置。
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            // IdentityServer
            #region IdentityServerAuthenticationOptions => need to refactor
            //Action<IdentityServerAuthenticationOptions> isaOptClient = option =>
            //{
            //    option.Authority = Configuration["IdentityService:Uri"];
            //    option.ApiName = "clientservice";
            //    option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
            //    option.SupportedTokens = SupportedTokens.Both;
            //    option.ApiSecret = Configuration["IdentityService:ApiSecrets:clientservice"];
            //};

            //Action<IdentityServerAuthenticationOptions> isaOptProduct = option =>
            //{
            //    option.Authority = Configuration["IdentityService:Uri"];
            //    option.ApiName = "productservice";
            //    option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
            //    option.SupportedTokens = SupportedTokens.Both;
            //    option.ApiSecret = Configuration["IdentityService:ApiSecrets:productservice"];
            //};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication("ClientServiceKey", isaOptClient)
            //    .AddIdentityServerAuthentication("ProductServiceKey", isaOptProduct);

            #endregion
                       
            #region Consul服务注册

            //services.Configure<ServiceRegisterOptions>(Configuration.GetSection("ServiceRegister"));
            //services.AddSingleton<IConsulClient>(p => new ConsulClient(cfg =>
            //{
            //    var serviceConfirguation = p.GetRequiredService<IOptions<ServiceRegisterOptions>>().Value;
            //    if (!string.IsNullOrEmpty(serviceConfirguation.Register.HttpEndpoint))
            //    {
            //        cfg.Address = new Uri(serviceConfirguation.Register.HttpEndpoint);
            //    }
            //}));

            #endregion

            #region 注入Swagger文档

            if (Configuration["Swagger:IsActive"] == bool.TrueString)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("GatewayService", new Info
                    {
                        Version = "v1",
                        Title = "Gateway API"
                    });
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath, "fx.ApiGateway.xml");
                    options.IncludeXmlComments(xmlPath);
                });
            }

            services.AddOcelot(new ConfigurationBuilder()
                    .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                    .Build())
                    .AddConsul();

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
        /// <param name="options"></param>
        /// <param name="consul"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IOptions<ServiceRegisterOptions> options)//, IConsulClient consul)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.ConsulApp(env, lifetime, options, consul);


            if (Configuration["Swagger:IsActive"] == bool.TrueString)
            {
                var apis = new List<string> { "OrderService", "ProductService", "AuthenticationService" };
                app.UseMvc()
                    .UseSwagger()
                    .UseSwaggerUI(o =>
                   {
                       apis.ForEach(m =>
                       {
                           o.SwaggerEndpoint($"/{m}/swagger.json", m);
                       });
                   });
            }
            app.UseCors("default");
            app.UseOcelot();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint($"/{Configuration["Swagger:DefineSwaggerName"]}/swagger.json", Configuration["Swagger:Version"]);
                c.SwaggerEndpoint("GatewayService/swagger.json", "Gateway API V1");
            });

            if(Configuration["Consul:IsActive"] == bool.TrueString)
            {
                app.RegisterConsul(lifetime, new ServiceEntity
                {
                    ConsulIP = Configuration["Consul:IP"],
                    ConsulPort = int.Parse(Configuration["Consul:Port"]),
                    IP = Configuration["Service:IP"],
                    Port = int.Parse(Configuration["Service:Port"]),
                    ServiceName = Configuration["Service:Name"]
                });
            }

            app.UseMvc();
        }
    }
}
