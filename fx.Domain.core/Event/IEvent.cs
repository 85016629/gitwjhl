using MediatR;
using System;

namespace fx.Domain.core
{
    public interface IEvent: IMessage, INotification
    {
        Guid EventId { get; set; }
        DateTime Timeline { get;}
    }
}
