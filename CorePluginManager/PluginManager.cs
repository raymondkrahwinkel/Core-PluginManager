using CorePluginManager.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager;

public static class PluginManager
{
    public static WebApplicationBuilder BuildPluginManager(this WebApplicationBuilder builder)
    {
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
            // todo: handle error
        }

        #region Helpers
        builder.Services.AddTransient<ISessionHelper, SessionHelper>();
        #endregion
        
        return builder;
    }
    
    public static IApplicationBuilder UsePluginManager(this IApplicationBuilder app)
    {
        app.UseSession();
        
        return app;
    }
}