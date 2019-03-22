using fx.Application.Customer;
using fx.Domain.Bus;
using fx.Domain.core;
using fx.Domain.CustomerContext;
using fx.Domain.CustomerContext.Commands;
using fx.Domain.CustomerContext.QueryStack.Repositoris;
using fx.Infra.Data.SqlServer;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Customer.WebApi.Confirguations
{
    /// <summary>
    /// 依赖注入配置。
    /// </summary>
    public static class IocSettings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationDISettings(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IMemoryBus, MediatBus>();
            services.AddScoped<IEventStore<DomainEvent>, EventStore>();

            services.AddScoped<IMediator, Mediator>();

            services.AddMediatR(typeof(Startup));
            //typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) }
            //services.AddScoped<IRequestHandler<UpdateLastLoginTimeCommand>, CustomerCommandExecutor>();
            services.AddScoped(typeof(IRequestHandler<RegisterCustomerCommand, object>), typeof(CustomerCommandExecutor));
            services.AddScoped(typeof(IRequestHandler<LoginCommand, object>), typeof(CustomerCommandExecutor));
        }
    }
}
