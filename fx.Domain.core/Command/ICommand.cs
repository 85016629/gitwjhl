using MediatR;
using System;

namespace fx.Domain.core
{
    public interface ICommand : IRequest<object>, IMessage
    {
        Guid CommandId { get; set; }
    }
}
