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
            var newOrder = new Order
            {
                UUId = request.Id,
                CreateTime = request.CreateTime,
                Owner = request.Owner,
                Status = request.State
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
