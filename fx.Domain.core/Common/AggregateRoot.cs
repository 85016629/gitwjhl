using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fx.Domain.core
{
    public abstract class AggregateRoot<TAggregateRootId> : IAggregateRoot
    {
        private IList<DomainEvent> _occurredEvents;
        private Queue<DomainEvent> _uncommittedEvents;
        public TAggregateRootId Id { get; set; }
        public IList<DomainEvent> OccurredEvents { get => _occurredEvents; set { _occurredEvents = value; } }
        /// <summary>
        /// 事件队列
        /// </summary>
        public Queue<DomainEvent> UncommittedEvents
        {
            get
            {
                if (this._uncommittedEvents == null)
                {
                    this._uncommittedEvents = new Queue<DomainEvent>();
                }
                return this._uncommittedEvents;
            }
        }

        public Guid UUId { get => Guid.NewGuid(); set => throw new NotImplementedException(); }

        public void ClearUncommittedEvents()
        {
            this._uncommittedEvents.Clear();
        }

        /// <summary>
        /// 通过反射委托引发事件。
        /// </summary>
        /// <param name="event"></param>
        public void RaiseEvent(DomainEvent @event)
        {
            bool flag = @event == null;
            if (flag)
            {
                throw new ArgumentNullException("event");
            }

            var eventHandlerMethods = from m in this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                      let parameters = m.GetParameters()
                                      where m.IsDefined(typeof(InlineEventHandlerAttribute)) &&
                                      m.ReturnType == typeof(void) &&
                                      parameters.Length == 1 &&
                                      parameters[0].ParameterType == @event.GetType()
                                      select m;

            @event.AggregateRootType = this.GetType().FullName;

            foreach (var eventHandlerMethod in eventHandlerMethods)
            {
                eventHandlerMethod.Invoke(this, new object[] { @event });
            }


            this.UncommittedEvents.Enqueue(@event);
        }

        /// <summary>
        /// 回放事件。
        /// </summary>
        /// <param name="events"></param>
        public void ReplayEvents(IEnumerable<DomainEvent> events)
        {
            bool flag = ((IAggregateRoot)this).OccurredEvents == null;
            if (flag)
            {
                ((IAggregateRoot)this).OccurredEvents = new List<DomainEvent>();
            }

            ClearUncommittedEvents();

            foreach (var evnt in events)
            {
                this.RaiseEvent(evnt);
            }
        }

        IEnumerable<DomainEvent> IAggregateRoot.GetUncommittedEvents()
        {
            return Enumerable.ToArray<DomainEvent>(this._uncommittedEvents);
        }
    }
}
