using System;
using System.Collections.Generic;
using System.Linq;
using CQRSFramework.CommandHandlers;
using CQRSFramework.Commands;
using CQRSFramework.Utils;

namespace CQRSFramework.Bus
{
    public class StructureMapCommandHandlerFactory : ICommandHandlerFactory
    {
        public IEnumerable<ICommandHandler<T>> GetHandlers<T>() where T : Command
        {
            var handlers = GetHandlerTypes<T>().ToList();

            var cmdHandlers = handlers.Select(handler =>
                (ICommandHandler<T>)ContainerBootstrapper.Container.GetInstance(handler)).ToList();

            return cmdHandlers;
        }

        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            var handlers = typeof(ICommandHandler<>).Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                    .Where(h => h.GetInterfaces()
                        .Any(ii => ii.GetGenericArguments()
                            .Any(aa => aa == typeof(T)))).ToList();


            return handlers;
        }
    }
}
