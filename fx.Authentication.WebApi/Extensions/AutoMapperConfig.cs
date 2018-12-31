using AutoMapper;
using fx.Domain.Customer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Authentication.WebApi.Extensions
{

    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public static class AutoMapperConfig
    {

        public static void AddAutoMapperSetup(this IServiceCollection services)
        {

            services.AddAutoMapper();

            //Mapper.Initialize(x => x.CreateMap<Customer, CustomerDto>()
            //.ForMember(dest => dest.LoginId, src => src.MapFrom(opt => opt.LoginId)));

            //Mapper.Initialize(x => x.CreateMap<CustomerDto, Customer>()
            //.ForMember(dest => dest.UserLoginId, src => src.Ignore()));
        }
    }
}
