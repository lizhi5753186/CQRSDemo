using System;

namespace CQRSFramework.Events
{
    public interface IDomainEvent : IEvent
    {
        Guid SourceId { get; }
    }
}
