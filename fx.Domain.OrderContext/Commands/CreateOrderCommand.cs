using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.OrderContext
{
    public class CreateOrderCommand : BaseCommand
    {
        public string Owner { get; set; }

        public IList<OrderItem> Items { get; set; }
        //public DateTime CreateTime { get; set; }
        //public OrderStatus State { get; set; }
        //public string Id { get; set; }


        //public CreateOrderCommand(string id, string owner, DateTime createTime, OrderStatus state)
        //{
        //    Id = id;
        //    Owner = owner;
        //    CreateTime = createTime;
        //    State = state;
        //    CommandId = Guid.NewGuid();
        //}        
    }
}
