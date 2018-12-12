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
using IdentityServer4.AccessTokenValidation;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // IdentityServer
            #region IdentityServerAuthenticationOptions => need to refactor
            Action<IdentityServerAuthenticationOptions> isaOptClient = option =>
            {
                option.Authority = Configuration["IdentityService:Uri"];
                option.ApiName = "clientservice";
                option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = Configuration["IdentityService:ApiSecrets:clientservice"];
            };

            Action<IdentityServerAuthenticationOptions> isaOptProduct = option =>
            {
                option.Authority = Configuration["IdentityService:Uri"];
                option.ApiName = "productservice";
                option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = Configuration["IdentityService:ApiSecrets:productservice"];
            };

            services.AddAuthentication()
                .AddIdentityServerAuthentication("ClientServiceKey", isaOptClient)
                .AddIdentityServerAuthentication("ProductServiceKey", isaOptProduct);

            #endregion
            
            services.AddOcelot(new ConfigurationBuilder()
                    .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                    .Build());

            #region Consul服务注册

            services.Configure<ServiceRegisterOptions>(Configuration.GetSection("ServiceRegister"));
            services.AddSingleton<IConsulClient>(p => new ConsulClient(cfg =>
            {
                var serviceConfirguation = p.GetRequiredService<IOptions<ServiceRegisterOptions>>().Value;
                if (!string.IsNullOrEmpty(serviceConfirguation.Register.HttpEndpoint))
                {
                    cfg.Address = new Uri(serviceConfirguation.Register.HttpEndpoint);
                }
            }));

            #endregion

            #region 注入Swagger

            if (Configuration["Swagger:IsActive"] == bool.TrueString)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(Configuration["Swagger:DefineSwaggerName"], new Info
                    {
                        Version = Configuration["Swagger:Version"],
                        Title = "Gatway API"
                    });

                    //Determine base path for the application.  
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    //Set the comments path for the swagger json and ui.  
                    var xmlPath = Path.Combine(basePath, "fx.ApiGateway.xml");
                    options.IncludeXmlComments(xmlPath);
                });
            }

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IOptions<ServiceRegisterOptions> options, IConsulClient consul)
        {
            //if(Configuration["ServiceRegister:IsActive"] == bool.TrueString)
            //{
            //    builder
            //}


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            app.ConsulApp(env, lifetime, options, consul);

            app.UseOcelot();

           
            //var apis = new List<string> { "OrderService" };
            //app.UseMvc()
            //    .UseSwagger()
            //    .UseSwaggerUI(o =>
            //   {
            //       apis.ForEach(m =>
            //       {
            //           o.SwaggerEndpoint($"/swagger/{m}/swagger.json", m);
            //       });
            //   });


            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "{documentName}/swagger.json";
            //});

            //app.UseSwaggerUI(c =>
            //{
            //    c.ShowExtensions();
            //    c.EnableValidator(null);
            //    c.SwaggerEndpoint($"/{Configuration["Swagger:DefineSwaggerName"]}/swagger.json", Configuration["Swagger:DefineSwaggerName"]);
            //});


        }
    }
}
