using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public class CommandExecutor : ICommandHandler<RegisterCustomerCommand>,
        ICommandHandler<UpdateLastLoginTimeCommand>
    {
        protected readonly IRepository<Customer, string> _storage;

        public CommandExecutor(IRepository<Customer, string> repository)
        {
            _storage = repository;
        }


        public async Task HandleAsync(UpdateLastLoginTimeCommand command)
        {
            var user = _storage.FindById(command.UserLoginId);
            user.UpdateLastLoginTime();
            await _storage.UpdateAsync(user);
        }

        public async Task HandleAsync(RegisterCustomerCommand command)
        {
            var customer = new Customer
            {
                LoginId = command.LoginId,
                Name = command.Name,
                RegisterTime = DateTime.Now,
                QQ = command.QQ
            };
           await _storage.Add(customer);
        }
    }
}
