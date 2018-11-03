using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public class EventStream
    {
        Guid Id { get; set; }
        IList<DomainEvent> EventList { get; set; }
    }
}
