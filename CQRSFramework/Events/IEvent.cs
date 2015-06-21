using System;

namespace CQRSFramework.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
