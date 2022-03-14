using CorePluginManager.Plugins.Breadcrumb.Models;

namespace CorePluginManager.Plugins.Breadcrumb;

public interface IBreadcrumbService
{
    /// <summary>
    /// Add step to the breadcrumb
    /// </summary>
    /// <param name="item"></param>
    void Add(BreadcrumbItem item);

    void Test();
}