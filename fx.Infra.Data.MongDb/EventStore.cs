namespace fx.Infra.Data.MongDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using fx.Domain.core;
    using MongoDB.Driver;
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
            var filter = Builders<DomainEvent>.Filter.AnyIn("DomainEvent.EventData", aggregateId);
            return dBContext.DbSet<DomainEvent>().FindSync(filter).ToList();
        }

        /// <summary>
        /// 保存事件。
        /// </summary>
        /// <param name="event"></param>
        public int SaveEvent(DomainEvent @event)
        {
            try
            {
                dBContext.DbSet<DomainEvent>().InsertOne(@event);
                return 1;
            }
            catch
            {
                return 0;
            }            
        }

        /// <summary>
        /// 保存事件。
        /// </summary>
        /// <param name="event"></param>
        public IMongoCollection<DomainEvent> GetAll()
        {
            return dBContext.DbSet<DomainEvent>();
        }
    }
}
