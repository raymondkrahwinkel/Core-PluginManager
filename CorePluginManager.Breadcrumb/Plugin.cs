using CorePluginManager.Breadcrumb.Services;
using CorePluginManager.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CorePluginManager.Breadcrumb;

public class Plugin : IPluginServiceCollection, IPluginApplicationBuilder
{
    public Version Version => new(0, 0, 1);
    public IApplicationBuilder AppBuilder(IApplicationBuilder app)
    {
        // find default breadcrumb item attribute
        BreadcrumbService.Initialize(PluginManager.Parent);
        
        return app;
    }

    public IServiceCollection AddPluginManager(IServiceCollection services)
    {
        services.AddScoped<IBreadcrumbService, BreadcrumbService>();
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        
        return services;
    }
}