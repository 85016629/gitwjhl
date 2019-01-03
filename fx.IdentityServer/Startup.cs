using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddSingleton<UserReporitory>();

            #region 配置IdentityServer

            InMemoryConfiguration.Configuration = this.Configuration;

            //var cert = new X509Certificate2(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Certificates\\idsrv4.pfx"), "111111");
            //X509SecurityKey key = new X509SecurityKey(cert);
            //SigningCredentials credentials = new SigningCredentials(key, "RS256");

            services.AddIdentityServer()
                //.AddSigningCredential
                //(
                //credentials
                ////new X509Certificate2
                ////(
                ////    Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Certificates\\idsrv4.pfx"),
                ////    "111111"
                ////)
                //)
                .AddDeveloperSigningCredential(false,"tempkey.rsa")
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
            app.UseMvcWithDefaultRoute();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
