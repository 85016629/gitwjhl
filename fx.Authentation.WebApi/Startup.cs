using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fx.Authentation.WebApi.Extensions;
using fx.Domain.Bus;
using fx.Domain.core;
using fx.Domain.Customer;
using fx.Infra.Data.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pivotal.Discovery.Client;

namespace fx.Authentation.WebApi
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDiscoveryClient(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMemoryBus, MediatBus>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEventStore<DomainEvent>, EventStore>();


            // Domain Bus (Mediator)

            //services.AddAutoMapperSetup();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Authentication API"
                });

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "fx.Authentation.WebApi.xml");
                options.IncludeXmlComments(xmlPath);
            });

            services.AddMediatR(typeof(Startup));

            services.AddScoped<INotificationHandler<LoginSuccessed>, CustomerEventHandler>();
            //typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) }
            //services.AddScoped<IRequestHandler<UpdateLastLoginTimeCommand>, CustomerCommandExecutor>();
            services.AddScoped(typeof(IRequestHandler<UpdateLastLoginTimeCommand, object>), typeof(CustomerCommandExecutor));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API V1");
            });

            app.UseDiscoveryClient();
        }
    }
}
