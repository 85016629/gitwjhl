using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IEventHandler<in T> where T : IMessage
    {
        Task HandleAsync(T @event);
        void Handle(T @event);        
    }
}
