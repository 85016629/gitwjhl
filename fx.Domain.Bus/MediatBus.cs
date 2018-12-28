using System.Threading.Tasks;
using fx.Domain.core;
using MediatR;

namespace fx.Domain.Bus
{
    public sealed class MediatBus : IMemoryBus
    {
        private readonly IEventStore<DomainEvent> _eventStore;
        private readonly IMediator _mediator;

        public MediatBus(IEventStore<DomainEvent> eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : DomainEvent
        {
            _eventStore.SaveEvent(@event);

            _mediator.Publish(@event);

            return Task.CompletedTask;
        }

        public Task<object> SendCommand<T>(T command) where T : BaseCommand
        {
            return _mediator.Send(command);
        }
    }
}