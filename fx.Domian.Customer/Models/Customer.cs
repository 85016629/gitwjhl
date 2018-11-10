namespace fx.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using fx.Domain.core;

    public class Customer : AggregateRoot<string>
    {
        private CustomerState _state;
        private string _name;

        public string Name { get => _name; set => _name = value; }
        public DateTime RegisterTime { get; set; }
        public string Mobile1 { get; set; }
        public string QQ { get; set; }
        public CustomerState State { get => _state; set => _state = value; }
        public string LoginId { get; set; }
        public DateTime LastLoginTime { get; set; }

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
