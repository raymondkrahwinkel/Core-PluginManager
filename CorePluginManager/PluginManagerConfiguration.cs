using Microsoft.Extensions.Configuration;

namespace CorePluginManager;

public class PluginManagerConfiguration
{
    #region Sessions
    /// <summary>
    /// Session idle timeout in minutes
    /// </summary>
    public long SessionIdleTimeout { get; set; }
    
    /// <summary>
    /// Session IO timeout in minutes
    /// </summary>
    public long SessionIOTimeout { get; set; }
    #endregion
    
    /// <summary>
    /// Gets configuration instance
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static PluginManagerConfiguration Load(IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection("PluginManager");
        var pluginManagerConfiguration = new PluginManagerConfiguration();

        pluginManagerConfiguration.SessionIdleTimeout = configurationSection.GetValue<long>("SessionIdleTimeout", 20);
        pluginManagerConfiguration.SessionIOTimeout = configurationSection.GetValue<long>("SessionIOTimeout", 1);
        
        return pluginManagerConfiguration;
    }
}