using fx.Domain.OrderContext.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.OrderContext
{
    public class OrderEventHandler : INotificationHandler<OrderCreated>,
        INotificationHandler<StockIsOverd>
    {
        public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            //订单创建成功，在这里可以向各个系统比如库存系统，监控系统发送消息
            return Task.CompletedTask;
        }

        /// <summary>
        /// 处理库存用完的事件
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(StockIsOverd notification, CancellationToken cancellationToken)
        {
            //并非一个事件对应一个命令，一个事件中可以发出多个命令，但是一个命令一般只发生一个事件。
            //比如通知供货商，给供货商发送短信
            //给供货商发送邮件
            return Task.CompletedTask;
        }
    }
}
