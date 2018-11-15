using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.OrderContext
{
    public interface IOrderService
    {
        Task<object> CreateOrder(Order order);
    }
}
