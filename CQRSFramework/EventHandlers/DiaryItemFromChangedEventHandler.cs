using CQRSFramework.Events;
using CQRSFramework.Storage;

namespace CQRSFramework.EventHandlers
{
    public class DiaryItemFromChangedEventHandler : IEventHandler<DiaryItemFromChangedEvent>
    {
        private readonly IStorage _queryStorage;
        public DiaryItemFromChangedEventHandler(IStorage queryStorage)
        {
            _queryStorage = queryStorage;
        }

        public void Handle(DiaryItemFromChangedEvent @event)
        {
            var item = _queryStorage.GetById(@event.SourceId);
            item.From = @event.From;
            item.Version = @event.Version;
        }
    }
}
