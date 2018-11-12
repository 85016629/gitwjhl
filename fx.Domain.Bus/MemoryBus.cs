using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fx.Domain.Bus
{
    public class MemoryBus : IMemoryBus
    {
        private Dictionary<Type, Type> dicCommandHandlers;
        private Dictionary<Type, Type> dicEventHandlers;

        private IEventStore<DomainEvent> _eventRepository;

        private IRepository<T, TKey> repository;

        public MemoryBus(IEventStore<DomainEvent> eventStore)
        {
            dicCommandHandlers = new Dictionary<Type, Type>();
            dicEventHandlers = new Dictionary<Type, Type>();
            _eventRepository = eventStore;
        }

        public  void RegisterEventHandler<TEvent, TEventHandler>()
        {
            if (!dicEventHandlers.ContainsKey(typeof(TEvent)))
                dicEventHandlers.Add(typeof(TEvent), typeof(TEventHandler));
        }

        public void RegisterCommandHandler<TCommand, TCommandHandler>()
        {
            if (!dicCommandHandlers.ContainsKey(typeof(TCommand)))
                dicCommandHandlers.Add(typeof(TCommand), typeof(TCommandHandler));
        }

        public async Task SendCommand<T>(T command) where T : ICommand
        {
            //if (dicCommandHandlers.ContainsKey(typeof(T)))
            //{
            //    var typeofCommandHandler = dicCommandHandlers[typeof(T)];
            //    var handler = (ICommandHandler<T>)Activator.CreateInstance(typeofCommandHandler, new object[1] { _repository });
            //}

            if (dicCommandHandlers.ContainsKey(typeof(T)))
            {
                var handler = (ICommandHandler<T>)Activator.CreateInstance(dicCommandHandlers[typeof(T)]);
               await  handler.HandleAsync(command);
            }
        }

        public async Task RaiseEvent<T>(T @event) where T : DomainEvent
        {
            if (dicEventHandlers.ContainsKey(typeof(T)))
            {
                await Task.Run(() => _eventRepository.SaveEvent(@event));
                var handler = (IEventHandler<T>)Activator.CreateInstance(dicEventHandlers[typeof(T)], this);
                await handler.HandleAsync(@event);
                
            }
        }
    }
}

