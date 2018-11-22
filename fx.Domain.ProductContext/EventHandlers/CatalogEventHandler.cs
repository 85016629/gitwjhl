using fx.Domain.core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.ProductContext
{
    public class CatalogEventHandler : 
        INotificationHandler<SubCatalogAdded>
    {
        private IMemoryBus _bus;
        public CatalogEventHandler(IMemoryBus bus)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(IMemoryBus));
        }
        public Task Handle(SubCatalogAdded @event, CancellationToken cancellationToken)
        {
            var cmd = new AddSubCatalogCommand(@event.CatalogName, @event.ParentId);
            _bus.SendCommand(cmd);
            return Task.CompletedTask;
        }
    }
}
