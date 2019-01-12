namespace fx.Domain.CustomerContext
{
    using fx.Domain.core;
    using System;
    using System.Threading.Tasks;

    public interface ICustomerRepository :  IRepository<Customer, Guid>
    {

    }
}
