using System;

namespace CQRSFramework.Events
{
    public class DiaryItemToChangedEvent : DomainEvent
    {
        public DateTime To { get; internal set; }
        public DiaryItemToChangedEvent(Guid aggregateId, DateTime to)
        {
            SourceId = aggregateId;
            To = to;
        }
    }
}
