using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IEventStore<in T> where T : DomainEvent
    {
        void SaveEvent(T @event);
        IEnumerable<IEvent> GetEventStreamByAggergateRootId(string id);

    }
}
