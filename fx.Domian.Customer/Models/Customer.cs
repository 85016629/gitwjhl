namespace fx.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using fx.Domain.core;

    public class Customer : AggregateRoot<string>
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }
        [Key]
        public string LoginId { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string Passwd { get; set; }
        
        public void UpdateLastLoginTime()
        {
            LastLoginTime = DateTime.Now;
        }

    }

    /// <summary>
    /// 客户状态
    /// </summary>
    public enum CustomerState
    {
        Common = 0,
        Lockedout = 1,
        Deleted
    }

    
}
