

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

        public async Task<object> CreateOrder(OrderViewModel order)
        {
            var cmd = new CreateOrderCommand()
            {
                Owner = order.Owner,
                Items = order.Items
            }; 
            return await Bus.SendCommand(cmd);
        }
    }
}
