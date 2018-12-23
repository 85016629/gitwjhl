using fx.Domain.OrderContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.SqlServer
{
    public class OrderRepository : BaseRepository<Order, string>, IOrderRepository
    {
        protected new readonly SqlDbContext dbContext = new SqlDbContext();
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

        public IQueryable<Order> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}