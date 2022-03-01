using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Interfaces;

public interface IPluginServiceCollection
{
    IServiceCollection AddPluginManager(IServiceCollection services);
}