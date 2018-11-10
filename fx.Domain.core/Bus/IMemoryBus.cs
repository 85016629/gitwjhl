using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IMemoryBus
    {
        void RegisterCommandHandler<TCommand, TCommandHandler>();
        void RegisterEventHandler<TEvent, TEventHandler>();
        void SendCommand<T>(T command) where T : ICommand;
        Task RaiseEvent<T>(T @event) where T : DomainEvent;
    }
}
