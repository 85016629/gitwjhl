using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fx.Domain.OrderContext
{
    public class Order : AggregateRoot<string>
    {
        private string _orderId;

        public string OrderId
        {
            get
            {
                if (string.IsNullOrEmpty(_orderId))
                {
                    return Guid.NewGuid().ToString();
                }
                return _orderId;
            }
            set => _orderId = value;
        }
        public string Owner { get; set; }
        public DateTime CreateTime { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus : byte
    {
        Processing = 0,
        Completed = 1
    }
}
