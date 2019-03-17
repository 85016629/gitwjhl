using AutoMapper;
using fx.Application.Customer.ViewModels;
using fx.Domain.core;
using fx.Domain.CustomerContext;
using fx.Domain.CustomerContext.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Application.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMemoryBus _bus;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository repository, IRoleRepository roleRepository, IMemoryBus bus, IMapper mapper)
        {
            _repository = repository;
            _bus = bus;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public void Dispose()
        {
            GC.Collect();
        }

        public async Task<Domain.CustomerContext.Customer> Login(LoginViewModel loginViewModel)
        {
            var loginCommand = _mapper.Map<LoginCommand>(loginViewModel);
            return (Domain.CustomerContext.Customer)await _bus.SendCommand(loginCommand);
        }

        public void Register(RegisterViewModel registerViewModel)
        {
            var registerCustomerCommand = _mapper.Map<RegisterCustomerCommand>(registerViewModel);
            _bus.SendCommand(registerCustomerCommand);
        }

        public bool ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            return _repository.ResetPassword(resetPasswordViewModel.LoginId, resetPasswordViewModel.NewPassword);
        }
    }
}
