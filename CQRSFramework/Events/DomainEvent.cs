using System;

namespace CQRSFramework.Events
{
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        public int Version;

        #region IDomainEvent Member
        public Guid SourceId { get; set; }
        #endregion 

        #region IEvent Members

        public Guid Id { get; set; }
        
        #endregion 
    }
}
