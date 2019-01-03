namespace fx.Domain.CustomerContext
{
    using System;
    using fx.Domain.core;
    public class LoginSuccessed : DomainEvent
    {
        public string LoginId { get; set; }
    }
}