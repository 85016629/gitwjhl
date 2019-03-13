using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Customer.WebApi.Configrations
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapSetup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapSetup(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }
    }
}
