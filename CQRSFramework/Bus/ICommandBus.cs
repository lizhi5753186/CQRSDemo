using CQRSFramework.Commands;

namespace CQRSFramework.Bus
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : Command;
    }
}
