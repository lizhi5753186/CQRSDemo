using System;

namespace CQRSFramework.Events
{
    public class DiaryItemRenamedEvent : DomainEvent
    {
        public string Title { get; internal set; }
        public DiaryItemRenamedEvent(Guid aggregateId, string title)
        {
			SourceId = aggregateId;
            Title = title;
        }
    }
}
