namespace fx.Application.Order.Interfaces
{
    using System.Threading.Tasks;
    using fx.Domain.OrderContext;

    public interface IOrderService
    {
        Task<object> CreateOrder(Order order);
        Task CancelOrder(string orderId);
    }
}
