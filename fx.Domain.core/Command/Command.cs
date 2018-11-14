using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public abstract class BaseCommand : ICommand
    {
        public Guid CommandId { get ; set ; }
    }
}
