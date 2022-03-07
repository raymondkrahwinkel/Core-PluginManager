using Microsoft.AspNetCore.Builder;

namespace CorePluginManager.Interfaces;

public interface IPluginApplicationBuilder : IPlugin
{
    IApplicationBuilder AppBuilder(IApplicationBuilder app);
}