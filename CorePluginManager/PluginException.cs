namespace CorePluginManager;

public class PluginException : Exception
{
    public PluginException(string plugin, string message) : base($"{plugin}: {message}")
    {
    }
    
    public PluginException(string plugin, Exception ex) : base($"{plugin}: {ex.Message}", ex)
    {
    }
}