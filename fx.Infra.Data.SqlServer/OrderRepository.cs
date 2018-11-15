using fx.Domain.OrderContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.SqlServer
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly SqlDbContext dbContext = new SqlDbContext();
        public void Add(Order entity)
        {
            dbContext.Orders.Add(entity);
            dbContext.SaveChanges();
        }

        public Task<int> AddAsync(Order entity)
        {
            dbContext.Orders.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public Order FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Order FindByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}