using CQRSFramework.Events;
using System;

namespace CQRSFramework.Domain
{
    public class DiaryItem : AggregateRoot,  
        IHandle<DiaryItemCreatedEvent>,
        IHandle<DiaryItemRenamedEvent>,
        IHandle<DiaryItemFromChangedEvent>,
        IHandle<DiaryItemToChangedEvent>,
        IHandle<DiaryItemDescriptionChangedEvent>
    {
        public string Title { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Description { get; set; }

        public DiaryItem()
        {
        }

        public DiaryItem(Guid id, string title, string description, DateTime from, DateTime to)
        {
            ApplyChange(new DiaryItemCreatedEvent(id, title, description, from, to));
        }

         public void ChangeTitle(string title)
        {
            ApplyChange(new DiaryItemRenamedEvent(ID, title));
        }

        public void ChangeDescription(string description)
        {
            ApplyChange(new DiaryItemDescriptionChangedEvent(ID, description));
        }

        public void ChangeFrom(DateTime from)
        {
            ApplyChange(new DiaryItemFromChangedEvent(ID, from));
        }

        public void ChangeTo(DateTime to)
        {
            ApplyChange(new DiaryItemToChangedEvent(ID, to));
        }

        public void Delete()
        {
            ApplyChange(new DiaryItemDeletedEvent(ID));
        }

        public void Handle(DiaryItemDeletedEvent e)
        {
            
        }

        public void Handle(DiaryItemCreatedEvent @event)
        {
            Title = @event.Title;
            From = @event.From;
            To = @event.To;
            ID = @event.SourceId;
            Description = @event.Description;
            Version = @event.Version;
        }

         public void Handle(DiaryItemRenamedEvent e)
        {
            Title = e.Title;
        }

        public void Handle(DiaryItemFromChangedEvent e)
        {
            From = e.From;
        }

        public void Handle(DiaryItemToChangedEvent e)
        {
            To = e.To;
        }

        public void Handle(DiaryItemDescriptionChangedEvent e)
        {
            Description = e.Description;
        }
    }
}
