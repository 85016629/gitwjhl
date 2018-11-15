using MediatR;
using System;

namespace fx.Domain.core
{
    public interface ICommand : IRequest<object>
    {
        Guid CommandId { get; set; }        
    }
}
