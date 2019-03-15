using fx.Domain.CustomerContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : BaseRepository<Customer, Guid>, ICustomerRepository
    {
        public Customer Login(string loginId, string password)
        {
            return dbContext.Customers
                .Where(c => c.LoginId == loginId && c.Password == password)
                .FirstOrDefault();
        }
    }
}
