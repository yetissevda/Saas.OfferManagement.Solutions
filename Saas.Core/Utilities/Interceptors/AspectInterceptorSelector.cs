using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Saas.Core.Aspect.Autofac.Exception;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Saas.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector :IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type,MethodInfo method,IInterceptor[] interceptors)
        {
            var classAttribute = type.GetCustomAttributes<MethodInterceptionBaseAttiribute>(true).ToList();
            var methodAttribute =
                (type.GetMethod(method.Name) ?? throw new InvalidOperationException()).GetCustomAttributes<MethodInterceptionBaseAttiribute>(true);
            classAttribute.AddRange(methodAttribute);
            classAttribute.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));
            //  classAttribute.Add(new ExceptionLogAspect(typeof(FileLogger)));
            // ReSharper disable once CoVariantArrayConversion
            return classAttribute.OrderBy(x => x.Priority).ToArray();
        }
    }
}
