using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.IdentityService
{
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Phone()
            };
        }

        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("api", "订单服务"),
                new ApiResource("profile", "产品服务")
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "ydapp.client",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowedScopes = new [] {
                        "profile",
                        "api" },
                    RedirectUris = { "http://localhost:5002/signin-oidc" },                                     //登录成功以后回调的接口，可以写在配置里面
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" }  //注销成功以后回调的接口，可以写在配置里面
                },
                new Client
                {
                    ClientId = "webapp.client",
                    ClientSecrets = new [] { new Secret("clowsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new [] { "api" }
                }
            };
        }
    }
}
