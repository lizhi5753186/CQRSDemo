using CQRSFramework.Events;

namespace CQRSFramework.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : DomainEvent;
    }
}
