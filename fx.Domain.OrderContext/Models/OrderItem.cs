using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.OrderContext
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string OrderId { get; set; }
        /// <summary>
        /// 产品编号。
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 数量。
        /// </summary>
        public int Quantity { get; set; }
    }
}
