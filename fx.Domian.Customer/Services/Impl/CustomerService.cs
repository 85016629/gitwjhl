using AutoMapper;
using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.Customer
{
    public class CustomerService 
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
    }
}
