using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public interface IDatabase
    {
        string ConnectString { get; set; }
    }
}
