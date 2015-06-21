using CQRSFramework.Domain;
using CQRSFramework.Events;
using CQRSFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using CQRSFramework.Events.Storage;
using CQRSFramework.Snapshots;

namespace CQRSFramework.Repositories
{
    // IDomainRepository的实现类
    public class DomainRepository<T> : IDomainRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStorage _storage;

        public DomainRepository(IEventStorage storage)
        {
            _storage = storage;
        }

        // 并没有直接对领域实体进行保存，而是先保存领域事件进EventStore，然后在Publish事件到EventBus进行处理
        // 然后EventBus把事件分配给对应的事件处理器进行处理，由事件处理器来把领域对象保存到QueryDatabase中
        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            if (aggregate.GetUncommittedChanges().Any())
            {
                _storage.Save(aggregate);
            }
        }

        public T GetById(Guid id)
        {
            IEnumerable<DomainEvent> events;

            // 从快照中查询最近发生一次领域事件
            var snapshot = _storage.GetSnapshot<BaseSnapshot>(id);
            if (snapshot != null)
            {
                // 如果快照存在，重建过程则从快照事件之后的事件开始重建，而不需要每次从最开始的事件进行重建
                events = _storage.GetEvents(id).Where(e => e.Version >= snapshot.Version);
            }
            else
            {
                // 从不存在则还是需要从最开始的对象开始重建
                events = _storage.GetEvents(id);
            }

            var obj = new T();
            if (snapshot != null)
                ((ISnapshotOrignator)obj).BuildFromSnapshot(snapshot); // 先应用快照中领域事件

            obj.LoadsFromHistory(events); // 逐个应用领域事件来重建对象
            return obj;
        }
    }
}
