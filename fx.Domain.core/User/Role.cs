using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public class Role : AggregateRoot<int>
    {
        public Role(string roleName)
        {
            RoleName = roleName;
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoles { get; set; } //一对多
    }
}
