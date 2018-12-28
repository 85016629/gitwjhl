using AutoMapper;
using fx.Domain.core;
using fx.Domain.Customer;
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
            throw new NotImplementedException();
        }
    }
}
