using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.OrderContext
{
    public class OrderCreated : DomainEvent
    {
        public string OrderId { get; set; }
        public string Owner { get; set; }
    }
}
