using fx.Application.Customer;
using fx.Domain.core;
using fx.Infra.Data.SqlServer.User;
using IdentityServer4.Services;
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<ITestLoginUserService, LoginUserService>();
            services.AddSingleton<TestUserReporitory>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScheme<IIdentityServerInteractionService, IdentityServerInteractionService>();
            #region 配置IdentityServer

            InMemoryConfiguration.Configuration = this.Configuration;

            //var cert = new X509Certificate2(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Certificates\\idsrv4.pfx"), "111111");
            //X509SecurityKey key = new X509SecurityKey(cert);
            //SigningCredentials credentials = new SigningCredentials(key, "RS256");

            var customUrl = "http://localhost:5000";

            services.AddIdentityServer(ids => {
                    ids.UserInteraction = new IdentityServer4.Configuration.UserInteractionOptions()
                    {
                        LoginUrl = customUrl + "/Account/Login",//【必备】登录地址  
                        LogoutUrl = customUrl + "/Account/Logout",//【必备】退出地址 
                        ConsentUrl = customUrl + "/Account/Consent",//【必备】允许授权同意页面地址
                        ErrorUrl = customUrl + "/Account/Error", //【必备】错误页面地址
                        LoginReturnUrlParameter = "returnUrl",//【必备】设置传递给登录页面的返回URL参数的名称。默认为returnUrl 
                        LogoutIdParameter = "logoutId", //【必备】设置传递给注销页面的注销消息ID参数的名称。缺省为logoutId 
                        ConsentReturnUrlParameter = "returnUrl", //【必备】设置传递给同意页面的返回URL参数的名称。默认为returnUrl
                        ErrorIdParameter = "errorId", //【必备】设置传递给错误页面的错误消息ID参数的名称。缺省为errorId
                        CustomRedirectReturnUrlParameter = "returnUrl", //【必备】设置从授权端点传递给自定义重定向的返回URL参数的名称。默认为returnUrl
                        CookieMessageThreshold = 5 //【必备】由于浏览器对Cookie的大小有限制，设置Cookies数量的限制，有效的保证了浏览器打开多个选项卡，一旦超出了Cookies限制就会清除以前的Cookies值
                    };
                })
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
                .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())                //添加身份认证资源
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
            app.UseCors(option =>
            {
                option.AllowCredentials();
                option.AllowAnyOrigin();
                option.AllowAnyHeader();
                option.AllowAnyMethod();
            });
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
