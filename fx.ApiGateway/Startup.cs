using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Extensions.PlatformAbstractions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.Swagger;

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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注入Swagger
          
            if(Configuration["Swagger:IsActive"] == bool.TrueString)
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

            services.Configure<ServiceRegisterOptions>(Configuration.GetSection("ServiceRegister"));
            services.AddSingleton<IConsulClient>(p => new ConsulClient( cfg =>
                {
                    var serviceConfirguation = p.GetRequiredService<IOptions<ServiceRegisterOptions>>().Value;
                    if (!string.IsNullOrEmpty(serviceConfirguation.Register.HttpEndpoint))
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

            app.ConsulApp(env, lifetime, options, consul);

            app.UseOcelot();


            var apis = new List<string> { "OrderApi" };
            app.UseMvc()
                .UseSwagger()
                .UseSwaggerUI(o =>
               {
                   apis.ForEach(m =>
                   {
                       o.SwaggerEndpoint($"/{m}/v1/swagger.json", m);
                   });
               });


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
