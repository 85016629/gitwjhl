using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IAggregateRoot
    {
        Guid UUId { get; set; }
        Queue<DomainEvent> UncommittedEvents { get;  }
        /// <summary>
        /// 获取未提交的事件
        /// </summary>
        /// <returns></returns>
        IEnumerable<DomainEvent> GetUncommittedEvents();
        //
        // 摘要:
        //     Gets or sets the list of the occurred events
        IList<DomainEvent> OccurredEvents { get; set; }
        /// <summary>
        /// 回放事件
        /// </summary>
        /// <param name="events"></param>
        void ReplayEvents(IEnumerable<DomainEvent> events);
        void RaiseEvent(DomainEvent @event);
        void ClearUncommittedEvents();
    }
}
