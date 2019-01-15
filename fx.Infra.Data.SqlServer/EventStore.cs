namespace fx.Infra.Data.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using fx.Domain.core;
    using Newtonsoft.Json;

    public class EventStore : IEventStore<DomainEvent>
    {
        private readonly EventDbContext dBContext;

        public EventStore()
        {
            dBContext = new EventDbContext();
        }

        public ICollection<DomainEvent> GetAllAggregateDomainEvents(string aggregateId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存事件。
        /// </summary>
        /// <param name="event"></param>
        public void SaveEvent(DomainEvent @event)
        {
            this.dBContext.DomainEventStorage.Add(@event);
            this.dBContext.SaveChanges();
        }
    }
}
