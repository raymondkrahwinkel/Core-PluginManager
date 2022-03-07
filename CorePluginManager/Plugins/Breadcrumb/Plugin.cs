using CorePluginManager.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Plugins.Breadcrumb;

public class Plugin : IPluginServiceCollection, IPluginApplicationBuilder
{
    public Version Version => new(0, 0, 1);
    public IApplicationBuilder AppBuilder(IApplicationBuilder app)
    {
        
        
        return app;
    }

    public IServiceCollection AddPluginManager(IServiceCollection services)
    {
        
        
        return services;
    }
}