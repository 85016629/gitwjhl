using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fx.Auth.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using HttpResponseExtensions = fx.Auth.Extensions.HttpResponseExtensions;

namespace fx.Auth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OAuthDefaults.DisplayName;
            })
            .AddCookie()
            .AddOAuth(OAuthDefaults.DisplayName, options =>
            {
                options.ClientId = "oauth.code";
                options.ClientSecret = "secret";
                options.TokenEndpoint = "https://oidc.faasx.com/connect/token";
                options.CallbackPath = "/signin-oauth";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.SaveTokens = true;
                options.AuthorizationEndpoint = "https://oidc.faasx.com/connect/authorize";
                // 事件执行顺序 ：
                // 1.创建Ticket之前触发
                options.Events.OnCreatingTicket = context => Task.CompletedTask;
                // 2.创建Ticket失败时触发
                options.Events.OnRemoteFailure = context => Task.CompletedTask;
                // 3.Ticket接收完成之后触发
                options.Events.OnTicketReceived = context => Task.CompletedTask;
                // 4.Challenge时触发，默认跳转到OAuth服务器
                // options.Events.OnRedirectToAuthorizationEndpoint = context => context.Response.Redirect(context.RedirectUri);
            });

            services.AddSingleton<UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //注册授权
            app.UseAuthentication();

            //app.Map("/Account/Login", builder => builder.Run(async context =>
            //{
            //    if (context.Request.Method == "GET")
            //    {
            //        await context.Response.WriteHtmlAsync(async res =>
            //        {
            //            await res.WriteAsync($"<form method=\"post\">");
            //            await res.WriteAsync($"<input type=\"hidden\" name=\"returnUrl\" value=\"{HttpResponseExtensions.HtmlEncode(context.Request.Query["ReturnUrl"])}\"/>");
            //            await res.WriteAsync($"<div class=\"form-group\"><label>用户名：<input type=\"text\" name=\"userName\" class=\"form-control\"></label></div>");
            //            await res.WriteAsync($"<div class=\"form-group\"><label>密码：<input type=\"password\" name=\"password\" class=\"form-control\"></label></div>");
            //            await res.WriteAsync($"<button type=\"submit\" class=\"btn btn-default\">登录</button>");
            //            await res.WriteAsync($"</form>");
            //        });
            //    }
            //    else
            //    {
            //        var userStore = context.RequestServices.GetService<UserRepository>();
            //        var user = userStore.FindUser(context.Request.Form["userName"], context.Request.Form["password"]);
            //        if (user == null)
            //        {
            //            await context.Response.WriteHtmlAsync(async res =>
            //            {
            //                await res.WriteAsync($"<h1>用户名或密码错误。</h1>");
            //                await res.WriteAsync("<a class=\"btn btn-default\" href=\"/Account/Login\">返回</a>");
            //            });
            //        }
            //        else
            //        {
            //            var claimIdentity = new ClaimsIdentity("Cookie");
            //            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            //            claimIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            //            claimIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            //            claimIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()));

            //            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            //            // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
            //            await context.SignInAsync(claimsPrincipal);
            //            if (string.IsNullOrEmpty(context.Request.Form["ReturnUrl"])) context.Response.Redirect("/");
            //            else context.Response.Redirect(context.Request.Form["ReturnUrl"]);
            //        }
            //    }
            //}));

            app.UseAuthorize();

            // 我的信息
            app.Map("/profile", builder => builder.Run(async context =>
            {
                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h1>你好，当前登录用户： {HttpResponseExtensions.HtmlEncode(context.User.Identity.Name)}</h1>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/Account/Logout\">退出</a>");

                    await res.WriteAsync($"<h2>AuthenticationType：{context.User.Identity.AuthenticationType}</h2>");

                    await res.WriteAsync("<h2>Claims:</h2>");
                    await res.WriteTableHeader(new string[] { "Claim Type", "Value" }, context.User.Claims.Select(c => new string[] { c.Type, c.Value }));

                    // 在第一章中介绍过HandleAuthenticateOnceAsync方法，在此调用并不会有多余的性能损耗。
                    var result = await context.AuthenticateAsync();
                    await res.WriteAsync("<h2>Tokens:</h2>");
                    await res.WriteTableHeader(new string[] { "Token Type", "Value" }, result.Properties.GetTokens().Select(token => new string[] { token.Name, token.Value }));
                });
            }));

            app.Map("/Account/Logout", builder => builder.Run(async context =>
            {
                await context.SignOutAsync();
                context.Response.Redirect("/");
            }));

           

            app.Run(async context =>
            {
                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h2>Hello OAuth Authentication</h2>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/profile\">我的信息</a>");
                });
            });
            //    app.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //}
        }
    }
}
