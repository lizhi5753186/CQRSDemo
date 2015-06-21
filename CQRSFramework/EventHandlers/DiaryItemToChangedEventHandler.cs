using CQRSFramework.Events;
using CQRSFramework.Services;
using CQRSFramework.Storage;

namespace CQRSFramework.EventHandlers
{
    public class DiaryItemToChangedEventHandler : IEventHandler<DiaryItemToChangedEvent>
    {
        private readonly IStorage _queryStorage;

        public DiaryItemToChangedEventHandler(IStorage queryStorage)
        {
            _queryStorage = queryStorage;
        }

        public void Handle(DiaryItemToChangedEvent @event)
        {
            var item = _queryStorage.GetById(@event.SourceId);
            item.To = @event.To;
            item.Version = @event.Version;
        }
    }
}