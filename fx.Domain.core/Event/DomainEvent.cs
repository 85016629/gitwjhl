
namespace fx.Domain.core
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public abstract class DomainEvent : IEvent
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
        public string EventData { get; set; }
        public string AggregateRootType { get; set; }
        public DomainEvent() { }

    }
}
