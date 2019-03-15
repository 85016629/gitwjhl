﻿using fx.Application.Customer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Application.Customer
{
    public interface ICustomerService : IDisposable
    {
        void Register(RegisterViewModel registerViewModel);

        Task<fx.Domain.CustomerContext.Customer> Login(LoginViewModel loginViewModel);
    }
}
