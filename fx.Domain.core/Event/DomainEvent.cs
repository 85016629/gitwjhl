
namespace fx.Domain.core
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using Newtonsoft.Json;

    public class DomainEvent : IEvent
    {
        private Guid _eventId;
        private DateTime _timeline = DateTime.Now;

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

        public DateTime Timeline { get => _timeline; set => _timeline = value; }
        public string EventData { get; set; }
        public string AggregateRootType { get; set; }
        public DomainEvent() { }

    }
}
