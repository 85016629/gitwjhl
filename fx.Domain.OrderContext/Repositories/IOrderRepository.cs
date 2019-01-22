using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.OrderContext
{
    public interface IOrderRepository : IRepository<Order, string>
    {
        Task CreateOrder(Order order);
    }
}
