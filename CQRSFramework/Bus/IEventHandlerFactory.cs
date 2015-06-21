using System.Collections.Generic;
using CQRSFramework.EventHandlers;
using CQRSFramework.Events;

namespace CQRSFramework.Bus
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : IEvent;
    }
}
