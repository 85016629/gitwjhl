using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public abstract class AggregateRoot<TKey> : IAggregateRoot
    {
        public TKey Id { get; set; }
        public Guid UUId { get => Guid.NewGuid(); set => throw new NotImplementedException(); }

    }
}
