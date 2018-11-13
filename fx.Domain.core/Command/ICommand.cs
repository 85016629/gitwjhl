using System;

namespace fx.Domain.core
{
    public interface ICommand : IMessage
    {
        Guid CommandId { get; set; }

        
    }
}
