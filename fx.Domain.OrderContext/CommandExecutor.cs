using fx.Domain.core;
using MediatR;
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
        public OrderCommandExecutor(IMemoryBus bus)
        {
            Bus = bus;       
        }

        public Task<object> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = new Order
            {
                Id = request.Id,
                CreateTime = request.CreateTime,
                Owner = request.Owner,
                State = request.State
            };


        }
    }
}
