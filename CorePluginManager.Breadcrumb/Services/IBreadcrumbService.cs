using CorePluginManager.Breadcrumb.Models;

namespace CorePluginManager.Breadcrumb.Services;

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

    /// <summary>
    /// Overwrite the existing items
    /// </summary>
    /// <param name="items"></param>
    void SetItems(List<BreadcrumbItem> items);
}