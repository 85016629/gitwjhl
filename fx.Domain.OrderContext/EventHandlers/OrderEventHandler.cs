using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.OrderContext
{
    public class OrderEventHandler : INotificationHandler<OrderCreated>
    {
        public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            //订单创建成功，在这里可以向各个系统比如库存系统，监控系统发送消息
            return Task.CompletedTask;
        }
    }
}
