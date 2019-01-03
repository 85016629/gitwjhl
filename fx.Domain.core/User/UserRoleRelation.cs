using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace fx.Domain.core
{
    public class UserRoleRelation
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Key]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual BaseUser User { get; set; } //为什么要用Virtual修饰？？
        public virtual Role Role { get; set; }
    }
}
