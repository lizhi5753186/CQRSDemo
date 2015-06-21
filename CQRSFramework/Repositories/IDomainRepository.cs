using CQRSFramework.Domain;
using System;

namespace CQRSFramework.Repositories
{
    public interface IDomainRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
