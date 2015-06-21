using CQRSFramework.Events;
using CQRSFramework.Services.ApplicationServices;
using CQRSFramework.Storage;

namespace CQRSFramework.EventHandlers
{
    // DiaryItemCreatedEvent的事件处理类
    public class DiaryIteamCreatedEventHandler : IEventHandler<DiaryItemCreatedEvent>
    {
        private readonly IStorage _storage;

        public DiaryIteamCreatedEventHandler(IStorage storage)
        {
            _storage = storage;
        }

        public void Handle(DiaryItemCreatedEvent @event)
        {
            var item = new DiaryItemDto()
            {
                Id = @event.SourceId,
                Description = @event.Description,
                From = @event.From,
                Title = @event.Title,
                To = @event.To,
                Version = @event.Version
            };

            // 将领域对象持久化到QueryDatabase中
            _storage.Add(item);
        }
    }
}
