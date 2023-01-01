using System.Linq;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Saas.Core.CrossCuttingConcerns.Caching;
using Saas.Core.Utilities.Interceptors;
using Saas.Core.Utilities.IoC;

namespace Saas.Core.Aspect.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private readonly int _duration;
        private readonly ICacheManager _cacheManager;



        public CacheAspect(int duration = 60)//dakika
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation?.Method?.ReflectedType?.FullName}.{invocation?.Method?.Name}");
            var argument = invocation?.Arguments.ToList();

            if (argument != null)
            {
                var key = $"{methodName}({string.Join(",", argument.Select(x => x?.ToString() ?? "<Null>"))})";
                if (_cacheManager.IsAdd(key))
                {
                    invocation.ReturnValue = _cacheManager.Get(key);
                    return;
                }
                invocation.Proceed();
                _cacheManager.Add(key, invocation.ReturnValue, _duration);
            }
        }


    }
}
