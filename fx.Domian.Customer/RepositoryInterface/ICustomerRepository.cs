namespace fx.Domain.CustomerContext
{
    using fx.Domain.core;
    using System;
    using System.Threading.Tasks;

    public interface ICustomerRepository :  IRepository<Customer, Guid>
    {
        Customer Login(string loginId, string password);
        bool ResetPassword(string loginId, string newPasswd);
    }
}
