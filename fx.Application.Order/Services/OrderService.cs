

namespace fx.Application.Order.Services
{
    using fx.Application.Order.Interfaces;
    using fx.Domain.core;
    using fx.Domain.OrderContext;
    using System.Threading.Tasks;
    public class OrderService : IOrderService
    {
        private readonly IMemoryBus Bus;
        public OrderService(IMemoryBus bus)
        {
            Bus = bus;
        }

        public async Task<object> CreateOrder(Order order)
        {
            var cmd = new CreateOrderCommand(order.UUId, order.Owner, order.CreateTime, order.Status);
            return await Bus.SendCommand(cmd);
        }
    }
}
