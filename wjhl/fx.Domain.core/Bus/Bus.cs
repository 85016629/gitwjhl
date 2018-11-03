using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public class Bus 
    {

        private static readonly Dictionary<Type, Type> dicCommandHandlers;
        private static readonly Dictionary<Type, Type> dicEventHandlers;
        private static readonly IEventStore<DomainEvent> eventStore = new EventStore();

        static Bus()
        {
            dicCommandHandlers = new Dictionary<Type, Type>();
            dicEventHandlers = new Dictionary<Type, Type>();
        }

        public static void RegisterEventHandler<TEvent, TEventHandler>()
        {
            if (!dicEventHandlers.ContainsKey(typeof(TEvent)))
                dicEventHandlers.Add(typeof(TEvent), typeof(TEventHandler));
        }

        public static void RegisterCommandHandler<TCommand, TCommandHandler>()
        {
            if (!dicCommandHandlers.ContainsKey(typeof(TCommand)))
                dicCommandHandlers.Add(typeof(TCommand), typeof(TCommandHandler));
        }

        public static Task RaiseEvent<T>(T @event) where T : IMessage
        {
            //foreach(var typeofEventHandler in dicEventHandlers.Values)
            //{
            //    var handler = (IEventHandler<T>)Activator.CreateInstance(typeofEventHandler, new object[1] { _repository });
            //    if (handler != null)
            //        handler.HandleAsync(@event);
            //}
            if (dicEventHandlers.ContainsKey(typeof(T)))
            {
                var handler = (IEventHandler<T>)Activator.CreateInstance(dicEventHandlers[typeof(T)]);
                eventStore.SaveEvent(@event as DomainEvent);
                handler.HandleAsync(@event);
            }
            return Task.CompletedTask;
        }

        public  static void SendCommand<T>(T command) where T : ICommand
        {
            //if (dicCommandHandlers.ContainsKey(typeof(T)))
            //{
            //    var typeofCommandHandler = dicCommandHandlers[typeof(T)];
            //    var handler = (ICommandHandler<T>)Activator.CreateInstance(typeofCommandHandler, new object[1] { _repository });
            //}

            if (dicCommandHandlers.ContainsKey(typeof(T)))
            {
                var handler = (ICommandHandler<T>)Activator.CreateInstance(dicCommandHandlers[typeof(T)]);
                handler.Handle(command);
            }
        }
    }
}
