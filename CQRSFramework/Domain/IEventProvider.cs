using CQRSFramework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSFramework.Domain
{
    public interface IEventProvider
    {
        void LoadsFromHistory(IEnumerable<DomainEvent> history);
        IEnumerable<DomainEvent> GetUncommittedChanges();
    }
}
