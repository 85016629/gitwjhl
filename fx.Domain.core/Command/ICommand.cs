using MediatR;
using System;

namespace fx.Domain.core
{
    public interface ICommand : IRequest<Unit>
    {
        Guid CommandId { get; set; }        
    }
}
