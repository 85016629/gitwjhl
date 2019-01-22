using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Order.WebApi
{
    public class IdentityServer4OAuth2OperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Security == null)
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                                        {
                                              {"oauth2", new List<string> { "openid", "profile", "UserServicesApi" }}
                                        };
            operation.Security.Add(oAuthRequirements);
        }
    }
}
