using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public abstract class Aggregate : IAggregate
    {
        private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public int Id { get; set; }
        public Aggregate(int id)
        {
            Id = id;
        }
        public Aggregate() { }

        private IList<DomainEvent> uncommittedEvents = null;
        private IList<DomainEvent> UncommittedEvents
        {
            get
            {
                bool flag = this.uncommittedEvents == null;
                if (flag)
                {
                    this.uncommittedEvents = new List<DomainEvent>();
                }
                return this.uncommittedEvents;
            }
        }

        public void RaiseEvent(DomainEvent @event)
        {
            bool flag = @event == null;
            if (flag)
            {
                throw new ArgumentNullException("event");
            }
            Guid? guid = ((IAggregate)this).TimelineId;
            bool hasValue = guid.HasValue;
            if (hasValue)
            {
                @event.TimelineId = guid;
            }

            this.UncommittedEvents.Add(@event);
        }

        /// <summary>
        /// 委托调用事件。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static object InvokeMemberOnType(Type type, object target, string name, object[] args)
        {
            try
            {
                // Try to incoke the method
                return type.InvokeMember(
                    name,
                    BindingFlags.InvokeMethod | bindingFlags,
                    null,
                    target,
                    args);
            }
            catch (MissingMethodException)
            {
                // If we couldn't find the method, try on the base class
                if (type.BaseType != null)
                {
                    return InvokeMemberOnType(type.BaseType, target, name, args);
                }
                //quick greg hack to allow methods to not exist!
                return null;
            }
        }

        public Guid? TimelineId { get; }

        public bool HasPendingChanges => throw new NotImplementedException();

        public IList<DomainEvent> OccurredEvents { get; set; }

        Guid IAggregate.Id { get; }

        public void ClearUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEvent> GetUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public void MarkEventAsSaved(DomainEvent @event)
        {
            throw new NotImplementedException();
        }

        public void ReplayEvents(IEnumerable<DomainEvent> occurredEvents)
        {
            throw new NotImplementedException();
        }
    }
}
