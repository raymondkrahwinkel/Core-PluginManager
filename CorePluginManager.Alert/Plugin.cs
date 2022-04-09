using CorePluginManager.Alert.Services;
using CorePluginManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Alert;

public class Plugin : IPluginServiceCollection
{
    public Version Version => new(0, 0, 1);
    
    public IServiceCollection AddPluginManager(IServiceCollection services)
    {
        services.AddTransient<IAlertService, AlertService>();
        
        return services;
    }
}