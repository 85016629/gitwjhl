using AutoMapper;
using fx.Domain.CustomerContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Customer.AutoMapper
{
    public class DomainCommandToDomainModelMappingProfile: Profile
    {
        public DomainCommandToDomainModelMappingProfile()
        {
            CreateMap<RegisterCustomerCommand, fx.Domain.CustomerContext.Customer>()
                .ForMember(dest => dest.LoginId, opt => opt.MapFrom(src => src.LoginId))
                .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name));
        }
    }
}
