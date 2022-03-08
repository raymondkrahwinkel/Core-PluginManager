using CorePluginManager.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Plugins.Breadcrumb;

public class Plugin : IPluginServiceCollection
{
    public Version Version => new(0, 0, 1);

    public IServiceCollection AddPluginManager(IServiceCollection services)
    {
        services.AddTransient<IBreadcrumbService, BreadcrumbService>();
        
        return services;
    }
}