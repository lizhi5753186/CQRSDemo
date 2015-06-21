using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSFramework.Commands
{
    public class ChangeItemCommand : Command
    {
        public DateTime To { get; internal set; }
        public DateTime From { get; internal set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }

        public ChangeItemCommand(Guid aggregateId, string title, string description, DateTime from, DateTime to, int version)
            : base(aggregateId, version)
        {
            To = to;
            From = from;
            Description = description;
            Title = title;
        }
    }
}
