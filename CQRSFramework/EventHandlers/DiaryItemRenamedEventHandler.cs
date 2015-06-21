using CQRSFramework.Events;
using CQRSFramework.Storage;

namespace CQRSFramework.EventHandlers
{
    public class DiaryItemRenamedEventHandler : IEventHandler<DiaryItemRenamedEvent>
    {
        private readonly IStorage _queryStorage;
        public DiaryItemRenamedEventHandler(IStorage queryStorage)
        {
            _queryStorage = queryStorage;
        }
        public void Handle(DiaryItemRenamedEvent @event)
        {
            var item = _queryStorage.GetById(@event.SourceId);
            item.Title = @event.Title;
            item.Version = @event.Version;
        }
    }
}
