using CQRSFramework.Events;
using CQRSFramework.Storage;

namespace CQRSFramework.EventHandlers
{
    public class DiaryItemDescriptionChangedEventHandler : IEventHandler<DiaryItemDescriptionChangedEvent>
    {
        private readonly IStorage _queryStorage;
        public DiaryItemDescriptionChangedEventHandler(IStorage queryStorage)
        {
            _queryStorage = queryStorage;
        }

        public void Handle(DiaryItemDescriptionChangedEvent @event)
        {
            var item = _queryStorage.GetById(@event.SourceId);
            item.Description = @event.Description;
            item.Version = @event.Version;
        }
    }
}
