using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public abstract class BaseUser : AggregateRoot<Guid>
    {
        public BaseUser()
        {
        }

        public string Username { get; set; }
        public Guid UUId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public DateTime RegisterTime { get; set; }
        //public UserRoleRelation Relation { get; set; }
        //public int RoleId { get; set; }
        public virtual ICollection<UserRoleRelation> UserRoles { get; set; } //一对多
    }
}
