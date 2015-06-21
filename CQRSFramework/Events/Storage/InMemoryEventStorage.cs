using System;
using System.Collections.Generic;
using System.Linq;
using CQRSFramework.Bus;
using CQRSFramework.Domain;
using CQRSFramework.Exceptions;
using CQRSFramework.Snapshots;
using CQRSFramework.Utils;

namespace CQRSFramework.Events.Storage
{
    // Event Store的实现，这里保存在内存中，通常是保存到具体的数据库中，如SQL Server、Mongodb等
    public class InMemoryEventStorage : IEventStorage
    {
        private readonly List<DomainEvent> _events;
        private readonly List<ISnapshot> _snapshots;

        private readonly IEventBus _eventBus;

        public InMemoryEventStorage(IEventBus eventBus)
        {
            _events = new List<DomainEvent>();
            _snapshots = new List<ISnapshot>();
            _eventBus = eventBus;
        }

        public IEnumerable<DomainEvent> GetEvents(Guid aggregateId)
        {
            var events = _events.Where(p => p.SourceId == aggregateId).Select(p => p);
            if (!events.Any())
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with AggregateRootId: {0} was not found", aggregateId));
            }
            return events;
        }

        // 领域事件的保存
        public void Save(AggregateRoot aggregate)
        {
            // 获得对应领域实体未提交的事件
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            
            foreach (var @event in uncommittedChanges)
            {
                version++;
                // 没3个事件创建一次快照
                if (version > 2)
                {
                    if (version % 3 == 0)
                    {
                        var originator = (ISnapshotOrignator)aggregate;
                        var snapshot = originator.CreateSnapshot();
                        snapshot.Version = version;
                        SaveSnapshot(snapshot);
                    }
                }

                @event.Version = version;
                // 保存事件到EventStore中
                _events.Add(@event);
            }

            // 保存事件完成之后，再将该事件发布到EventBus 做进一步处理
            foreach (var @event in uncommittedChanges)
            {
                var desEvent = TypeConverter.ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }

        public T GetSnapshot<T>(Guid aggregateId) where T : BaseSnapshot
        {
            var memento = _snapshots.Where(m => m.AggregateRootId == aggregateId).Select(m => m).LastOrDefault();
            if (memento != null)
                return (T)memento;
            return null;
        }

        public void SaveSnapshot(ISnapshot snapshot)
        {
            _snapshots.Add(snapshot);
        }
    }
}
