using AutoMapper;
using fx.Domain.core;
using fx.Domain.CustomerContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : BaseRepository<Customer, Guid>, ICustomerRepository
    {
        protected new readonly SqlDbContext dbContext = new SqlDbContext();

        public Customer FindByLoginId(string userLoginId)
        {
            return dbContext.Customers.Where(u => u.LoginId == userLoginId).FirstOrDefault();
        }

        public Customer QueryCustomerByIdAndPwd(string userLoginId, string passwd)
        {
            return dbContext.Customers.Where(u => u.LoginId == userLoginId && u.Password == passwd).FirstOrDefault();
        }
    }
}
