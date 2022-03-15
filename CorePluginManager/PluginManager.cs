using System.Reflection;
using CorePluginManager.Helpers;
using CorePluginManager.Interfaces;
using CorePluginManager.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager;

public static class PluginManager
{
    private static Assembly _parentAssembly = null!;
    public static Assembly Parent => _parentAssembly;
    
    public static WebApplicationBuilder BuildPluginManager(this WebApplicationBuilder builder, Assembly parentAssembly)
    {
        // store the parent assembly
        _parentAssembly = parentAssembly;
        
        // load configuration
        builder.Services.Configure<PluginManagerConfiguration>(builder.Configuration.GetSection("PluginManager"));
        var configuration = PluginManagerConfiguration.Load(builder.Configuration);

        try
        {
            // add needed services
            builder.Services.AddHttpContextAccessor();

            // add session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(configuration.SessionIdleTimeout);
                options.IOTimeout = TimeSpan.FromMinutes(configuration.SessionIOTimeout);
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during setup PluginManager: {ex.StackTrace}");
        }

        #region Helpers
        builder.Services.AddTransient<ISessionHelper, SessionHelper>();
        #endregion
        
        // start loading plugins
        var assemblies = Assemblies.AssemblyTypesByInterface(typeof(IPluginServiceCollection));
        if (assemblies.Any())
        {
            foreach(var assembly in assemblies)
            {
                var instance = (IPluginServiceCollection)Activator.CreateInstance(assembly)!;
                try
                {
                    instance?.AddPluginManager(builder.Services);
                }
                catch (Exception ex)
                {
                    // todo: handle exception
                }
            }
        }
        
        return builder;
    }
    
    public static IApplicationBuilder UsePluginManager(this IApplicationBuilder app)
    {
        app.UseSession();
        
        // start loading plugins
        var assemblies = Assemblies.AssemblyTypesByInterface(typeof(IPluginApplicationBuilder));
        if (assemblies.Any())
        {
            foreach(var assembly in assemblies)
            {
                var instance = (IPluginApplicationBuilder)ActivatorUtilities.CreateInstance(app.ApplicationServices, assembly);
                try
                {
                    instance?.AppBuilder(app);
                }
                catch (Exception ex)
                {
                    // todo: handle exception
                }
            }
        }
        
        return app;
    }
}