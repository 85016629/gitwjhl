using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public interface IAggregate
    {
        //
        // 摘要:
        //     获取聚合Id
        Guid Id { get; }
        //
        // 摘要:
        //     获取实例所属的时间线Id
        Guid? TimelineId { get; }
        //
        // 摘要:
        //     指定聚合是否具有挂起的更改。
        bool HasPendingChanges { get; }
        //
        // 摘要:
        //     获取或设置发生事件的列表。
        IList<DomainEvent> OccurredEvents { get; set; }

        //
        // 摘要:
        //    清除聚合的未提交事件列表。
        void ClearUncommittedEvents();
        //
        // 摘要:
        //     获取聚合的未提交事件列表。
        //
        // 返回结果:
        //     未提交事件列表
        IEnumerable<DomainEvent> GetUncommittedEvents();
        //
        // 摘要:
        //     标记发生的未提交事件。
        //
        // 参数:
        //   event:
        //     标记发生的未提交事件。
        void MarkEventAsSaved(DomainEvent @event);
        //
        // 摘要:
        //     重放指定的事件列表
        //
        // 参数:
        //   occurredEvents:
        //     要重放的事件列表
        void ReplayEvents(IEnumerable<DomainEvent> occurredEvents);
    }
}
