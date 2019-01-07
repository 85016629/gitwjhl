using fx.Domain.core;
using fx.Infra.Data.SqlServer.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.BackMng.Base.WebApi.Extensions
{
    public static class IocConfig
    {
        public static void AddBusinessIoc(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
