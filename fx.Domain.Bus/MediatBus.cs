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

        public void RegisterCommandHandler<TCommand, TCommandHandler>()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterEventHandler<TEvent, TEventHandler>()
        {
            throw new System.NotImplementedException();
        }

        public Task<object> SendCommand<T>(T command) where T : ICommand
        {
            return _mediator.Send(command);
        }
    }
}