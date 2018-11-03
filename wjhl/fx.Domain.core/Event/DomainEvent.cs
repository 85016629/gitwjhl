
namespace fx.Domain.core
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class DomainEvent : IEvent
    {
        private Guid _eventId;
        [Key]
        public Guid EventId
        {
            get
            {
                return Guid.Empty.Equals(_eventId) ? Guid.NewGuid() : _eventId;
            }
            set
            {
                _eventId = value;
            }
        }
        
        public DateTime Timeline { get; set; }
        public Guid? TimelineId { get; set; }
        public string AggregateRootId { get; set; }
        public string EventData { get; set; }
        public string AggregateRootType { get; set; }
        public string EventType { get; set; }
        public DomainEvent() { }
        public DomainEvent(IAggregateRoot aggregate)
        {
            this.EventData = JsonConvert.SerializeObject(aggregate);
            this.AggregateRootType = aggregate.GetType().ToString();
            this.EventId = aggregate.UUId;
        }
    }

    /// <summary>
    /// Represents that the decorated method is an event handler within an aggregate root.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class InlineEventHandlerAttribute : Attribute
    {

    }
}
