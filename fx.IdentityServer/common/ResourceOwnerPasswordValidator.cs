using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace fx.IdentityService
{

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private ITestLoginUserService loginUserService;

        public ResourceOwnerPasswordValidator(ITestLoginUserService _loginUserService)
        {
            this.loginUserService = _loginUserService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            bool isAuthenticated = loginUserService.Authenticate(context.UserName, context.Password, out LoginUser loginUser);
            //bool isAuthenticated = context.UserName == "aaa" && context.Password == "1" ? true : false;
            if (!isAuthenticated)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid client credential");
            }
            else
            {
                context.Result = new GrantValidationResult(
                    subject: context.UserName,
                    authenticationMethod: "custom",
                    claims: new Claim[] {
                        new Claim("Name", context.UserName),
                        //new Claim("Id", string.Empty),
                        //new Claim("RealName", string.Empty),
                        //new Claim("Email",string.Empty)
                        new Claim("Id", loginUser.Id.ToString()),
                        new Claim("RealName", loginUser.RealName),
                        new Claim("Email",loginUser.Email),
                        new Claim("Role", "admin")
                    }
                );
            }

            return Task.CompletedTask;
        }
    }
}
