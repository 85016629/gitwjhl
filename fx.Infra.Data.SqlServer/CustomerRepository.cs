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


        public Customer FindById(string id)
        {
            //var entityDto = dbContext.Customers.Where(u => u.LoginId == id).FirstOrDefault();
            var entity = dbContext.Customers.Find(id);
            return entity;
        }


        public void Add(Customer entity)
        {
            dbContext.Customers.Add(entity);
            dbContext.SaveChanges();
        }

        public Task<int> AddAsync(Customer entity)
        {
            dbContext.Customers.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public int Update(Customer entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public Customer FindByIds(object[] ids)
        {
            var entity = dbContext.Customers.Find(ids);
            return entity;
        }

        public IQueryable<Customer> GetAll()
        {
            return null;
        }
    }
}
