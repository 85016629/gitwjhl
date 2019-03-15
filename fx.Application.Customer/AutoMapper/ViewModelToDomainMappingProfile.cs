using AutoMapper;
using fx.Application.Customer.ViewModels;
using fx.Domain.CustomerContext;
using fx.Domain.CustomerContext.Commands;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterCustomerCommand>()
                .ConstructUsing(c => new RegisterCustomerCommand(c.Username));

            CreateMap<LoginViewModel, LoginCommand>()
                .ConstructUsing(c => new LoginCommand(c.LoginId, c.Password));
        }
    }
}
