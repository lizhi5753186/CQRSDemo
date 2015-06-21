using CQRSFramework.Events;

namespace CQRSFramework.Bus
{
    // EventBus的实现
    public class EventBus : IEventBus
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;

        public EventBus(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Publish<T>(T @event) where T : DomainEvent
        {
            // 获得对应的EventHandle来处理事件
            var handlers = _eventHandlerFactory.GetHandlers<T>();
            foreach (var eventHandler in handlers)
            {
                // 对事件进行处理
                eventHandler.Handle(@event);
            }
        }
    }
}
