using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.OrderContext
{
    public class OrderService : IOrderService
    {
        private readonly IMemoryBus Bus;
        public OrderService(IMemoryBus bus)
        {
            Bus = bus;
        }
        public Task<object> CreateOrder(Order order)
        {
            var cmd = new CreateOrderCommand(order.UUId, order.Owner, order.CreateTime, order.State);
            return Bus.SendCommand(cmd);
        }
    }
}
