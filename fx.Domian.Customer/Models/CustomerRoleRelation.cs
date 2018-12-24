using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.Customer.Models
{
    public class CustomerRoleRelation : AggregateRoot<Guid>
    {
        public Role Role { get; set; }
        public Customer Customer { get; set; }
        public int RoleId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
