using fx.Domain.ProductContext;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Product.WebApi
{
    public static class CommandEventRegister
    {
        /// <summary>
        /// 注册领域命令。
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCommand(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRequestHandler<AddParentCatalogCommand, object>), typeof(CatalogCommandExecutor));
            services.AddScoped(typeof(IRequestHandler<AddSubCatalogCommand, object>), typeof(CatalogCommandExecutor));
        }

        /// <summary>
        /// 注册领域事件
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterEvent(this IServiceCollection services)
        {
            services.AddScoped(typeof(INotificationHandler<SubCatalogAdded>), typeof(CatalogEventHandler));
        }
    }
}
