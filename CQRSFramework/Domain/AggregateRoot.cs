using CQRSFramework.Events;
using CQRSFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSFramework.Domain
{
    public abstract class AggregateRoot : IEventProvider, IAggregateRoot
    {
        private readonly List<DomainEvent> _changes;

        #region IEntity Members
        public Guid ID { get; internal set; }
        #endregion 

        public int Version { get; internal set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<DomainEvent>();
        }

        #region IEventProvider Members

        public void LoadsFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var e in history) 
                ApplyChange(e, false);

            Version = history.Last().Version;
            EventVersion = Version;
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }
        #endregion 

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        protected void ApplyChange(DomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(DomainEvent @event, bool isNew)
        {
            dynamic d = this;

            d.Handle(TypeConverter.ChangeTo(@event, @event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }
        }      
    }
}
