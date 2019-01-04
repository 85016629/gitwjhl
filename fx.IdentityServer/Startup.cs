using fx.Application.Customer;
using fx.Domain.core;
using fx.Infra.Data.SqlServer.User;
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

            //services.AddScoped<ILoginUserService, LoginUserService>();
            //services.AddSingleton<UserReporitory>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IUserRepository, UserRepository>();
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
                .AddInMemoryIdentityResources(Config.GetIdentityResources())                //添加身份认证资源
                .AddInMemoryClients(InMemoryConfiguration.GetClients())                        //预置允许访问的客户端
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())  //配置访问的API资源              
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
            //启用IdentifyServer
            app.UseIdentityServer();
            //启用UI
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
