using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public class Bus : IBus
    {

        private readonly Dictionary<Type, Type> dicCommandHandlers;
        private readonly Dictionary<Type, Type> dicEventHandlers;
        private readonly IEventStore<DomainEvent> eventStore;

        public Bus()
        {
            dicCommandHandlers = new Dictionary<Type, Type>();
            dicEventHandlers = new Dictionary<Type, Type>();
        }

        public void RegisterEventHandler<TEvent, TEventHandler>()
        {
            if (!dicEventHandlers.ContainsKey(typeof(TEvent)))
                dicEventHandlers.Add(typeof(TEvent), typeof(TEventHandler));
        }

        public void RegisterCommandHandler<TCommand, TCommandHandler>()
        {
            if (!dicCommandHandlers.ContainsKey(typeof(TCommand)))
                dicCommandHandlers.Add(typeof(TCommand), typeof(TCommandHandler));
        }

        public Task RaiseEvent<T>(T @event) where T : IEvent
        {
            if (dicEventHandlers.ContainsKey(typeof(T)))
            {
                var handler = (IEventHandler<T>)Activator.CreateInstance(dicEventHandlers[typeof(T)]);
                eventStore.SaveEvent(@event as DomainEvent);
                handler.HandleAsync(@event);
            }
            return Task.CompletedTask;
        }

        public  void SendCommand<T>(T command) where T : ICommand
        {

            if (dicCommandHandlers.ContainsKey(typeof(T)))
            {
                var handler = (ICommandHandler<T>)Activator.CreateInstance(dicCommandHandlers[typeof(T)]);
                handler.Handle(command);
            }
        }
    }
}
