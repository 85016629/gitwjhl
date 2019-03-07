using AutoMapper;
using fx.Application.Customer.ViewModels;
using fx.Domain.core;
using fx.Domain.CustomerContext;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void Register(RegisterViewModel registerViewModel)
        {
            var registerCustomerCommand = _mapper.Map<RegisterCustomerCommand>(registerViewModel);

            _bus.SendCommand(registerCustomerCommand);
        }
    }
}
