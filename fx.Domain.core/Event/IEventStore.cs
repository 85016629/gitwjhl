using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IEventStore<in T> where T : DomainEvent
    {
        int SaveEvent(T @event);

        ICollection<DomainEvent> GetAllAggregateDomainEvents(string aggregateId);
    }
}
