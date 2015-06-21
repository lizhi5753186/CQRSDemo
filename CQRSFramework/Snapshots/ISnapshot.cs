using System;

namespace CQRSFramework.Snapshots
{
    public interface ISnapshot
    {
        Guid AggregateRootId { get; }
        long Version { get; set; }
    }
}
