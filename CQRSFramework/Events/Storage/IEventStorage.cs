using System;
using System.Collections.Generic;
using CQRSFramework.Domain;
using CQRSFramework.Snapshots;

namespace CQRSFramework.Events.Storage
{
    public interface IEventStorage
    {
        IEnumerable<DomainEvent> GetEvents(Guid aggregateId);
        void Save(AggregateRoot aggregate);

        T GetSnapshot<T>(Guid aggregateId) where T : BaseSnapshot;
        void SaveSnapshot(ISnapshot snapshot);
    }
}
