using AutoMapper;
using fx.Application.Customer.ViewModels;
using fx.Domain.CustomerContext;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterCustomerCommand>()
                .ConstructUsing(c => new RegisterCustomerCommand(c.Username));
        }
    }
}
