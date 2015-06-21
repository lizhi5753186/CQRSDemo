using CQRSFramework.Commands;

namespace CQRSFramework.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
