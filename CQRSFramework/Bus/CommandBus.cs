using CQRSFramework.Commands;

namespace CQRSFramework.Bus
{
    // CommandBus 的实现
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public void Send<T>(T command) where T : Command
        {
            // 获得对应的CommandHandle来对命令进行处理
            var handlers = _commandHandlerFactory.GetHandlers<T>();

            foreach (var handler in handlers)
            {
                // 处理命令
                handler.Execute(command);
            }
        }       
    }
}
