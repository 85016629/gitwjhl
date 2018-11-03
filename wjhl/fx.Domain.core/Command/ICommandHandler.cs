using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface ICommandHandler<in T> where T : IMessage
    {
        Task HandleAsync(T command);
        void Handle(T command);
    }
}
