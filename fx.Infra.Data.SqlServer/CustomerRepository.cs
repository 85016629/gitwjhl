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

        public async Task Delete(string id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            dbContext.Remove(entityDto);
            await dbContext.SaveChangesAsync();
        }

        public Customer FindById(int id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Customer>(entityDto);
        }

        public Customer FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> FindByIdAsync(int id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Task<Customer>>(entityDto);
        }

        public Task<Customer> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Customer QueryCustomerByIdAndPwd(string userLoginId, string passwd)
        {
            var entityDto = dbContext.FindAsync<CustomerDto>(userLoginId, passwd);
            return Mapper.Map<Customer>(entityDto);
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
