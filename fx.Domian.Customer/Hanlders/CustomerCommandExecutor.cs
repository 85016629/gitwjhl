using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public class CustomerCommandExecutor : ICommandHandler<RegisterCustomerCommand>,
        ICommandHandler<UpdateLastLoginTimeCommand>
    {
        protected ICustomerRepository _storage;
        protected IMemoryBus _bus;

        public CustomerCommandExecutor(ICustomerRepository repository, IMemoryBus bus)
        {
            _storage = repository;
            _bus = bus;
        }


        public Task HandleAsync(UpdateLastLoginTimeCommand command)
        {
            var user = _storage.FindById(command.UserLoginId);
            user.UpdateLastLoginTime();
            if (_storage.Update(user) > 0)
            {

            }

            return Task.CompletedTask;
        }

        public async Task HandleAsync(RegisterCustomerCommand command)
        {
            var customer = new Customer
            {
                LoginId = command.LoginId,
                Name = command.Name,
            };
           _storage.Add(customer);
        }
    }
}
