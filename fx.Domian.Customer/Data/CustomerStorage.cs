namespace fx.Domain.Customer
{
    using AutoMapper;
    using System;
    using System.Threading.Tasks;
    public class CustomerStorage : ICustomerRepository
    {
        protected readonly CustomerDbContext dbContext = new CustomerDbContext();
        public Customer FindById(int id)
        {
            var entityDto =  dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Customer>(entityDto);
        }

        public Task<Customer> FindByIdAsync(int id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Task<Customer>>(entityDto);
        }

        public int SaveChange(Customer entity)
        {
            var e =  Mapper.Map<CustomerDto>(entity);
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

