namespace fx.Domain.CustomerContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using fx.Domain.core;

    public class Customer : BaseUser
    {
        public VipLevel VipLevel { get; set; }
    }
    public enum VipLevel : byte
    {
        Silver,
        Gold,
        Diamond
    }
}
