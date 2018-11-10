

namespace fx.Domain.Customer
{
    using System;
    using fx.Domain.core;
    public class LoginSuccessed : DomainEvent
    {
        public Guid EventId { get; set; }
        public DateTime Timeline => DateTime.Now;
        public string CustomerId { get; set; }
    }
}