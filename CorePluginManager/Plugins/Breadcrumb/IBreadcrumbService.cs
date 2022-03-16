using System.Reflection;
using CorePluginManager.Plugins.Breadcrumb.Models;

namespace CorePluginManager.Plugins.Breadcrumb;

public interface IBreadcrumbService
{
    /// <summary>
    /// Gets list with all current items
    /// </summary>
    List<BreadcrumbItem> Items { get; }
    
    /// <summary>
    /// Add step to the breadcrumb
    /// </summary>
    /// <param name="item"></param>
    void Add(BreadcrumbItem item);

    // todo: add overwrite function to inject own List<BreadcrumbItem>
}