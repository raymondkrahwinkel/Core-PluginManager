using CorePluginManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Plugins.Alert;

public class Plugin : IPlugin, IPluginServiceCollection
{
    public Version Version => new(0, 0, 1);
    
    public IServiceCollection AddPluginManager(IServiceCollection services)
    {
        services.AddTransient<IAlertService, AlertService>();
        
        return services;
    }
}