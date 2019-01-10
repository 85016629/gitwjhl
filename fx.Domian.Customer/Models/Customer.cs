namespace fx.Domain.CustomerContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using fx.Domain.core;

    /// <summary>
    /// 继承于BaseUser，由于EFCore只支持TPH继承模式，所以这里所有的用户公用一个表。
    /// 等微软想通了把EFCore支持TPT模式了，再修改。
    /// </summary>
    [Table("User")]
    public class Customer : BaseUser
    {
        public VipLevel VipLevel { get; set; }
    }

    public enum VipLevel
    {
        Silver,
        Gold,
        Diamond
    }
}
