using System;

namespace CQRSFramework.Snapshots
{
    public abstract class BaseSnapshot : ISnapshot
    {
        public Guid AggregateRootId { get; internal set; }
        public long Version { get; set; }
    }
}
