using fx.Domain.CustomerContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : BaseRepository<Customer, Guid>, ICustomerRepository
    {

    }
}
