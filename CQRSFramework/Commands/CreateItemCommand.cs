using System;

namespace CQRSFramework.Commands
{
    public class CreateItemCommand : Command
    {
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public DateTime From { get; internal set; }
        public DateTime To { get; internal set; }

        public CreateItemCommand(Guid aggregateId, string title,
            string description, int version, DateTime from, DateTime to)
            : base(aggregateId, version)
        {
            Title = title;
            Description = description;
            From = from;
            To = to;
        }
    }
}
