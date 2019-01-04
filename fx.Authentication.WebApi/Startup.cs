using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fx.Authentication.WebApi.Extensions;
using fx.Domain.Bus;
using fx.Domain.core;
using fx.Domain.CustomerContext;
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
//using Pivotal.Discovery.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using fx.Application.Customer;

namespace fx.Authentication.WebApi
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
            //services.AddDiscoveryClient(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMemoryBus, MediatBus>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEventStore<DomainEvent>, EventStore>();


            // Domain Bus (Mediator)

            //services.AddAutoMapperSetup();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("AuthenticationService", new Info
                {
                    Version = "v1",
                    Title = "认证服务"
                });

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "fx.Authentication.WebApi.xml");
                options.IncludeXmlComments(xmlPath);
            });

            services.AddMediatR(typeof(Startup));

            services.AddScoped<INotificationHandler<LoginSuccessed>, CustomerEventHandler>();
            //typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) }
            //services.AddScoped<IRequestHandler<UpdateLastLoginTimeCommand>, CustomerCommandExecutor>();
            services.AddScoped(typeof(IRequestHandler<UpdateLastLoginTimeCommand, object>), typeof(CustomerCommandExecutor));

            //services.AddMvcCore()
            //    .AddAuthorization()
            //    .AddJsonFormatters();
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.Authority = "http://localhost:4000";
            //        options.ApiName = "api";
            //    });

            #region Jwt授权认证

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }) 
            .AddJwtBearer(o =>
            {
                o.Authority = Configuration["Authorize:ServerUri"];
                o.Audience = "api";
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidAudience = "api",
                    ValidateAudience = true

                    //ValidIssuer = "http://localhost:5000",
                    //ValidAudience = "api",
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret1"))

                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    // RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                    // ValidateAudience = true,
                    // ValidateIssuer = true, 
                    // ValidateIssuerSigningKey = false,
                    // 是否要求Token的Claims中必须包含Expires
                    // RequireExpirationTime = true,
                    // 允许的服务器时间偏移量
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    // ValidateLifetime = true
                };
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("AuthenticationService/swagger.json", "Authentication API V1");
            });

            //app.UseDiscoveryClient();

            app.UseAuthentication();

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
