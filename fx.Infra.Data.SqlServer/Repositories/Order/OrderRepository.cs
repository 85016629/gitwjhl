using fx.Domain.OrderContext;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// 分页示例。
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public IList<Order> QueryOrdersByPage(int pageIndex, int pageSize, out int totalRecords)
        {
            var result = db.Orders
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            totalRecords = db.Orders.Count();
            return result;
        }

        /// <summary>
        /// Like查询示例。
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="owner"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public IList<Order> SearchOrdersByPage(int pageIndex, int pageSize, string owner,  out int totalRecords)
        {
            var result = db.Orders
                .Where(c => EF.Functions.Like(c.Owner, "%t%"))                
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            //var result = db.Orders.FromSql("").ToList();

            totalRecords = db.Orders
                .Count(c => EF.Functions.Like(c.Owner, "%t%"));

            return result;
        }
    }
}