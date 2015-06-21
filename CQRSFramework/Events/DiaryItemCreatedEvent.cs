using System;

namespace CQRSFramework.Events
{
    public class DiaryItemCreatedEvent : DomainEvent
    {
        public string Title { get; internal set; }
        public DateTime From { get; internal set; }
        public DateTime To { get; internal set; }
        public string Description { get;internal set; }

        public DiaryItemCreatedEvent(Guid aggregateId, string title,
            string description, DateTime from, DateTime to)
        {
			SourceId = aggregateId;
            Title = title;
            From = from;
            To = to;
            Description = description;
        }
    }
}
