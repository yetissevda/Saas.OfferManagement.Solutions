using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Saas.Core.CrossCuttingConcerns.Caching;
using Saas.Core.Utilities.Interceptors;
using Saas.Core.Utilities.IoC;

namespace Saas.Core.Aspect.Autofac.Caching
{
    public class CasheRemoveAspect :MethodInterception
    {
        private readonly string _pattern;
        private readonly ICacheManager _cacheManager;

        public CasheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
