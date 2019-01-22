using fx.Domain.OrderContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Order.ViewModels
{
    public class OrderViewModel
    {
        public string Owner { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
