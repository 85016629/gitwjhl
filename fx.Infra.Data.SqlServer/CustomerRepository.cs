using AutoMapper;
using fx.Domain.core;
using fx.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly CustomerDbContext dbContext = new CustomerDbContext();

        public Customer QueryCustomerByIdAndPwd(string userLoginId, string passwd)
        {
            return dbContext.Customers.Where(u => u.LoginId == userLoginId && u.Passwd == passwd).FirstOrDefault();
        }

        public Task<int> AddAsync<T>(T entity)
        {
            var e = entity as Customer;

            dbContext.Customers.Add(e);
            return dbContext.SaveChangesAsync();
        }

        public Customer FindById(string id)
        {
            //var entityDto = dbContext.Customers.Where(u => u.LoginId == id).FirstOrDefault();
            var entity = dbContext.Customers.Find(id);
            return entity;
        }


        public int Update<T>(T entity)
        {
            var e = entity as Customer;
            dbContext.Entry(e).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public void Add<T>(T entity) where T : IAggregateRoot
        {
            var e = entity as Customer;

            dbContext.Customers.Add(e);
            dbContext.SaveChanges();
        }
    }
}
