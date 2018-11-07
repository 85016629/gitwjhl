using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public interface IMessageQueue
    {
        string Hostname { get; set; }
        string Username { get; set; }
        string Passwd { get; set; }
        int Port { get; set; }
    }
}
