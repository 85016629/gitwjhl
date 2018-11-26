using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public abstract class AggregateRoot<TKey> : IAggregateRoot, IMemoryBus
    {
        [Key]
        public TKey Id { get; set; }

        public Task RaiseEvent<T>(T @event) where T : DomainEvent
        {
            throw new NotImplementedException();
        }

        public Task<object> SendCommand<T>(T command) where T : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
