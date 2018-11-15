using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.OrderContext
{
    public class Order : AggregateRoot<string>
    {
        public string Owner { get; set; }
        public DateTime CreateTime { get; set; }
        public int State { get; set; }
    }
}
