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

        public void Handle(RegisterCustomerCommand command)
        {
            var customer = new Customer
            {
                LoginId = command.LoginId,
                IDNumber = command.IDNumber,
                Name = command.Name,
                RegisterTime = DateTime.Now,
                QQ = command.QQ,
                Remarks = command.Remarks
            };

            _storage.Add(customer);
        }


        public async Task HandleAsync(UpdateLastLoginTimeCommand command)
        {
            var user = _storage.FindById(command.UserLoginId);

        }

        public Task HandleAsync(RegisterCustomerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
