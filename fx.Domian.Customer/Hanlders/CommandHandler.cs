using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.Customer
{
    public class CommandHandler : ICommandHandler<RegisterCustomerCommand>
    {
        protected readonly IRepository<Customer> _storage;

        public CommandHandler(IRepository<Customer> repository)
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

            _storage.SaveChange(customer);
        }

        public Task HandleAsync(RegisterCustomerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
