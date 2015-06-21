using System;

namespace CQRSFramework.Events
{
    public class DiaryItemDescriptionChangedEvent : DomainEvent
    {
        public string Description { get; internal set; }
        public DiaryItemDescriptionChangedEvent(Guid aggregateId, string description)
        {
			SourceId = aggregateId;
            Description = description;
        }
    }
}
