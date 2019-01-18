using fx.Domain.core;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.OrderContext
{
    public class OrderCommandExecutor:
        IRequestHandler<CreateOrderCommand, object>
    {
        private readonly IMemoryBus Bus;
        private readonly IOrderRepository OrderRepository;
        public OrderCommandExecutor(IMemoryBus bus, IOrderRepository orderRepository)
        {
            Bus = bus;
            OrderRepository = orderRepository;
        }

        public async Task<object> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            //这里可以放入检查库存的事件
            //if(!CheckProductStock()){
            //  如果库存用完
            //  发生StockIsOverd事件
            //}

            var newOrder = new Order
            {
                UUId = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                Owner = request.Owner,
                Status = OrderStatus.Processing
            };

            var r = await OrderRepository.AddAsync(entity: newOrder);

            var orderCreatedEvent = new OrderCreated()
            {
                AggregateRootType = nameof(Order),
                EventData = JsonConvert.SerializeObject(newOrder),
                Owner = newOrder.Owner,
                OrderId = newOrder.OrderId
            };

            await Bus.RaiseEvent(orderCreatedEvent);

            return Task.FromResult((object)newOrder);
        }
    }
}
