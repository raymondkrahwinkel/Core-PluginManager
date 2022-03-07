using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Interfaces;

public interface IPluginServiceCollection : IPlugin
{
    IServiceCollection AddPluginManager(IServiceCollection services);
}