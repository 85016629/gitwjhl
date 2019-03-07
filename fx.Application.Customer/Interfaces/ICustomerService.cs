using fx.Application.Customer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Customer
{
    public interface ICustomerService : IDisposable
    {
        void Register(RegisterViewModel registerViewModel);
    }
}
