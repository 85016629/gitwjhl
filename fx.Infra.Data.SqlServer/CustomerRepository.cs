using AutoMapper;
using fx.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly CustomerDbContext dbContext = new CustomerDbContext();
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

        public int SaveChange(Customer entity)
        {
            var e = Mapper.Map<CustomerDto>(entity);
            dbContext.Add(e);
            return dbContext.SaveChanges();
        }

        public Task<int> SaveChangeAsync(Customer entity)
        {
            dbContext.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public int Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
