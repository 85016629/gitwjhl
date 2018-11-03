

namespace fx.Domain.core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class EventStore : IEventStore<DomainEvent>
    {
        private readonly EventDBContext dBContext = new EventDBContext("MembershipContext");
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
            var newEvent = new DomainEvent()
            {
                AggregateRootId = @event.AggregateRootId,
                AggregateRootType = @event.AggregateRootType,
                EventData = JsonConvert.SerializeObject(@event),
                TimelineId = @event.TimelineId == null ? Guid.NewGuid() : @event.TimelineId,
                Timeline = DateTime.Now
            };

            this.dBContext.DomainEventStorage.Add(newEvent);
            this.dBContext.SaveChanges();
        }
    }
}
