using System;

namespace CQRSFramework.Events
{
    public class DiaryItemDeletedEvent : DomainEvent
    {
        public DiaryItemDeletedEvent(Guid aggregateId)
        {
			SourceId = aggregateId;
        }
    }
}
