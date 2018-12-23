using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public class Role : IAggregateRoot
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
