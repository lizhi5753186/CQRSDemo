using CQRSFramework.Events;
using CQRSFramework.Storage;

namespace CQRSFramework.EventHandlers
{
    public class DiaryItemDeletedEventHandler : IEventHandler<DiaryItemDeletedEvent>
    {
        private readonly IStorage _queryStorage;
        public DiaryItemDeletedEventHandler(IStorage queryStorage)
        {
            _queryStorage = queryStorage;
        }

        public void Handle(DiaryItemDeletedEvent @event)
        {
            _queryStorage.Delete(@event.SourceId);
        }
    }
}
