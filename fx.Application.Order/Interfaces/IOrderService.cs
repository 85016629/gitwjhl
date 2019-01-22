

namespace fx.Application.Order.Interfaces
{
    using System.Threading.Tasks;
    using fx.Application.Order.ViewModels;
    using fx.Domain.OrderContext;

    public interface IOrderService
    {

        Task CancelOrder(string orderId);

        Task<fx.Domain.OrderContext.Order> CreateOrder(OrderViewModel order);

    }
}
