using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IAggregateRoot
    {
        //Guid UUId { get; set; }
        //Queue<DomainEvent> UncommittedEvents { get;  }
        //IEnumerable<DomainEvent> GetUncommittedEvents();
        //IList<DomainEvent> OccurredEvents { get; set; }
        ///// <param name="events"></param>
        //void ReplayEvents(IEnumerable<DomainEvent> events);
        //void RaiseEvent(DomainEvent @event);
        //void ClearUncommittedEvents();
    }
}
