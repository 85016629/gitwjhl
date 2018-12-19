﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace fx.IdentityService
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

            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddSingleton<UserReporitory>();

            #region 配置IdentityServer

            InMemoryConfiguration.Configuration = this.Configuration;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(InMemoryConfiguration.GetClients())
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()          //添加自定义验证
                .AddProfileService<ProfileService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseIdentityServer();
            //启用UI
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();

            //app.UseHttpsRedirection();
            //app.UseMvc();
        }
    }
}
