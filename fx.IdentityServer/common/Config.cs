using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.IdentityService
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
        // 省略的客户端...

        // OIDC 隐式流客户端（MVC）
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "Mvc 客户端",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // 登录后重定向到的地址
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    // 注销后重定向到的地址
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
