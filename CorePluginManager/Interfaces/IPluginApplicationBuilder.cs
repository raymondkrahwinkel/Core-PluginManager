using Microsoft.AspNetCore.Builder;

namespace CorePluginManager.Interfaces;

public interface IPluginApplicationBuilder
{
    IApplicationBuilder AppBuilder(IApplicationBuilder app);
}