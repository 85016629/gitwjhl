using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fx.Domain.OrderContext
{
    public class Order : AggregateRoot<string>
    {
        [Key]
        public new string Id { get; set; }
        public string Owner { get; set; }
        public DateTime CreateTime { get; set; }
        public int State { get; set; }
    }
}
