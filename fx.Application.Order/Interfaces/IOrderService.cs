using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using fx.Application.Order.ViewModels;
using fx.Domain.OrderContext;

namespace fx.Application.Order.Interfaces
{
    public interface IOrderService
    {
        Task<fx.Domain.OrderContext.Order> CreateOrder(OrderViewModel order);
    }
}
