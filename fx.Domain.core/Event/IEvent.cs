using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IEvent: IMessage
    {
        Guid EventId { get; set; }
        DateTime Timeline { get;}
    }
}
