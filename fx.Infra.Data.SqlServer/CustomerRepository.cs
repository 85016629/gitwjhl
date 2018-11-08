using AutoMapper;
using fx.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly CustomerDbContext dbContext = new CustomerDbContext();

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer FindById(int id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Customer>(entityDto);
        }

        public Task<Customer> FindByIdAsync(int id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Task<Customer>>(entityDto);
        }

        public Customer QueryByIdAndPwd(string userLoginId, string passwd)
        {
            var entityDto = dbContext.FindAsync<CustomerDto>(userLoginId, passwd);
            return Mapper.Map<Customer>(entityDto);
        }

        public Task<Customer> QueryCustomerByIdAndPwd(string userLoginId, string passwd)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public Task<int> UpdateAsync(Customer entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }
    }
}
