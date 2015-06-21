using CQRSFramework.Bus;
using CQRSFramework.Events.Storage;
using CQRSFramework.Repositories;
using CQRSFramework.Storage;
using StructureMap;

namespace CQRSFramework
{
    // 应用程序初始化操作，将依赖的对象通过依赖注入框架StructureMap进行注入
    public sealed class ServiceLocator
    {
        private static readonly ICommandBus _commandBus;
        private static readonly IStorage _queryStorage;
        private static readonly bool IsInitialized;
        private static readonly object LockThis = new object();
        
        static ServiceLocator()
        {
            if (!IsInitialized)
            {
                lock (LockThis)
                {
                    // 依赖注入
                    ContainerBootstrapper.BootstrapStructureMap();

                    _commandBus = ContainerBootstrapper.Container.GetInstance<ICommandBus>();
                    _queryStorage = ContainerBootstrapper.Container.GetInstance<IStorage>();
                    IsInitialized = true;
                }
            }
        }

        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }

        public static IStorage QueryStorage
        {
            get { return _queryStorage; }
        }
    }

    class ContainerBootstrapper
    {
        private static Container _container;
        public static void BootstrapStructureMap()
        {
            _container = new Container(x =>
            {
                x.For(typeof (IDomainRepository<>)).Singleton().Use(typeof (DomainRepository<>));
                x.For<IEventStorage>().Singleton().Use<InMemoryEventStorage>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IStorage>().Use<InMemoryStorage>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
            });
        }

        public static Container Container 
        {
            get { return _container;}
        }
    }
}
