using System;

namespace CQRSFramework.Snapshots
{
    public class DiaryItemSnapshot : BaseSnapshot
    {
        public string Title { get; internal set; }
        public string Description { get;internal set; }
        public DateTime From { get; internal set; }
        public DateTime To { get; internal set;}
        
        public int EventVersion { get; set; }

        public DiaryItemSnapshot(Guid aggregateRootId, string title,string description, DateTime from, DateTime to,int version)
        {
            Title = title;
            AggregateRootId = aggregateRootId;
            Title = title;
            From = from;
            To = to;
            Version = version;
            EventVersion = version;
            Description = description;
        }
    }
}
