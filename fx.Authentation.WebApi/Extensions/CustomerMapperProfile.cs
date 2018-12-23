using AutoMapper;
using fx.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Authentation.WebApi.Extensions
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {

            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.UserLoginId, source => source.MapFrom(src => src.LoginId))
                .ForMember(dest => dest.Username, source => source.MapFrom(src => src.Name));
        }
    }
}
