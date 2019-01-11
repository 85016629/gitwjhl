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
        private readonly SqlDbContext db = new SqlDbContext();
        public async Task CreateOrder(Order order)
        {
            foreach(var item in order.OrderItems)
            {
                db.OrderItems.Add(item);
            }

            db.Orders.Add(order);

            await db.SaveChangesAsync();
        }
    }
}