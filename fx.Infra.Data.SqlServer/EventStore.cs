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

        /// <summary>
        /// 根据聚合Id查询所有事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<IEvent> GetEventStreamByAggergateRootId(string id)
        {
            return dBContext.DomainEventStorage.Where<DomainEvent>(x => x.AggregateRootId == id).ToList<DomainEvent>();
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
