using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Saas.Core.CrossCuttingConcerns.Caching;
using Saas.Core.CrossCuttingConcerns.Caching.Microsoft;
using Saas.Core.CrossCuttingConcerns.Mapper.AutoMapper;
using Saas.Core.Utilities.IoC;
using System.Diagnostics;

namespace Saas.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            //services.AddSingleton<ICacheManager,RedisCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingModels());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //        services.AddSingleton<IMailingManager, FluentEmailManager>();
            //services.AddSingleton<>()
        }
    }
}
