using AutoMapper;
using fx.Application.Customer.ViewModels;
using fx.Domain.CustomerContext;

namespace Equinox.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, RegisterViewModel>();
        }
    }
}
