using System;
using System.Collections.Generic;
using System.Linq;
using CQRSFramework.EventHandlers;
using CQRSFramework.Events;

namespace CQRSFramework.Bus
{
   public class StructureMapEventHandlerFactory : IEventHandlerFactory
    {
       public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : IEvent
        {
            var handlers = GetHandlerType<T>();

            var lstHandlers = handlers.Select(handler => 
                (IEventHandler<T>)ContainerBootstrapper.Container.GetInstance(handler)).ToList();
            return lstHandlers;
        }

       private static IEnumerable<Type> GetHandlerType<T>() where T : IEvent
        {
            var handlers = typeof(IEventHandler<>).Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                    .Where(h => h.GetInterfaces()
                        .Any(ii => ii.GetGenericArguments()
                            .Any(aa => aa == typeof(T)))).ToList();

            return handlers;
        }
    }
}
