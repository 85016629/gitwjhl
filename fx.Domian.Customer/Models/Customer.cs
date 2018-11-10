namespace fx.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using fx.Domain.core;

    public class Customer : AggregateRoot<string>
    {
        private CustomerState _state;
        private string _name;
        private string _id;

        public string Name { get => _name; set => _name = value; }
        public string Id { get => _id; set => _id = value; }
        public DateTime RegisterTime { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string QQ { get; set; }
        public CustomerState State { get => _state; set => _state = value; }
        public string LoginId { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }
        public string Remarks { get; set; }

        public Guid UUId { get ; set ; }

        public Queue<DomainEvent> UncommittedEvents => throw new NotImplementedException();

        public IList<DomainEvent> OccurredEvents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ClearUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEvent> GetUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public void RaiseEvent(DomainEvent @event)
        {
            throw new NotImplementedException();
        }

        public void ReplayEvents(IEnumerable<DomainEvent> events)
        {
            throw new NotImplementedException();
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
