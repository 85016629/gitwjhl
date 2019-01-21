

namespace fx.Application.Order.Services
{
    using fx.Application.Order.Interfaces;
    using fx.Application.Order.ViewModels;
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

        public async Task<Order> CreateOrder(OrderViewModel vmOrder)
        {
            var cmd = new CreateOrderCommand()
            {
                Owner = vmOrder.Owner,
                Items = vmOrder.Items
            };
            var order = await Bus.SendCommand(cmd) as Order;
            return order;
        }
    }
}
