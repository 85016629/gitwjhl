using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Domain.core
{
    public abstract class BaseUser : IAggregateRoot
    {
        public BaseUser()
        {
        }

        public Guid UserId { get; set; }
        public string UserLoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime LastUptDatetime { get; set; }
        public DateTime CreateTime { get; set; }
        public Role Role { get; set; }
    }
}
