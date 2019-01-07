using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public class BaseUser : AggregateRoot<Guid>
    {
        public string Username { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public DateTime RegisterTime { get; set; }
        //public UserRoleRelation Relation { get; set; }
        //public int RoleId { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoles { get; set; } //一对多
    }
}
