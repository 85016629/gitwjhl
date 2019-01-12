using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using fx.Domain.OrderContext;

namespace fx.Application.Order.Interfaces
{
    public interface IOrderService
    {
        Task<object> CreateOrder(fx.Domain.OrderContext.Order order);
    }
}
