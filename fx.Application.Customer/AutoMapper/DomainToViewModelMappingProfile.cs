using AutoMapper;
using fx.Application.Customer.ViewModels;
using fx.Domain.CustomerContext;

namespace Equinox.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterCustomerCommand>();

            CreateMap<RegisterCustomerCommand, Customer>()
                .ForMember(des => des.LoginId, opt => opt.MapFrom(src => src.LoginId))
                .ForMember(des => des.Username, opt => opt.MapFrom(src => src.Name))
                .ForMember(des => des.MobilePhone, opt => opt.MapFrom(src => src.Mobile));
        }
    }
}
