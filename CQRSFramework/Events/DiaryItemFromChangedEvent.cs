using System;

namespace CQRSFramework.Events
{
    public class DiaryItemFromChangedEvent : DomainEvent
    {
        public DateTime From { get; internal set; }
        public DiaryItemFromChangedEvent(Guid aggregateId, DateTime from)
        {
			SourceId = aggregateId;
            From = from;
        }
    }
}
