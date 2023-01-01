using System;
using Microsoft.Extensions.DependencyInjection;

namespace Saas.Core.Utilities.IoC
{
    public static class ServiceTool
    {
        //.net coreun tum servislerini cagirabiliriz.
        public static IServiceProvider ServiceProvider {get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }



    }
}
