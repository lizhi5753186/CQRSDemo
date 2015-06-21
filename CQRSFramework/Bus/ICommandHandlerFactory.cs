using System.Collections.Generic;
using CQRSFramework.CommandHandlers;
using CQRSFramework.Commands;

namespace CQRSFramework.Bus
{
    public interface ICommandHandlerFactory
    {
        IEnumerable<ICommandHandler<T>> GetHandlers<T>() where T : Command;
    }
}
