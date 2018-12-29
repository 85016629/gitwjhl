using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public class UserRoleRelation
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public virtual BaseUser User { get; set; } //为什么要用Virtual修饰？？
        public virtual Role Role { get; set; }
    }
}
