using Castle.DynamicProxy;
using System;

namespace Saas.Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public class MethodInterceptionBaseAttiribute :Attribute, IInterceptor
    {
        public virtual void Intercept(IInvocation invocation)
        {

        }

        public int Priority { get; set; }

    }
}
