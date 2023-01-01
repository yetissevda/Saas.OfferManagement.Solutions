using Microsoft.Extensions.DependencyInjection;

namespace Saas.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}
