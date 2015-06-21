using CQRSFramework.Events;

namespace CQRSFramework.EventHandlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}
