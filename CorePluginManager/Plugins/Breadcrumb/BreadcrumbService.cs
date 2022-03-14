using CorePluginManager.Plugins.Breadcrumb.Models;

namespace CorePluginManager.Plugins.Breadcrumb;

public class BreadcrumbService : IBreadcrumbService
{
    private List<BreadcrumbItem> _items { get; } = new();

    public BreadcrumbService()
    {
        // search and add the default item
        
    }

    public void Add(BreadcrumbItem item)
    {
        if (item.IsDefault && _items.Any(x => x.IsDefault))
        {
            throw new PluginException("Breadcrumb", "there can only be 1 default breadcrumb item");
        }
        
        _items.Add(item);
    }

    public void Test()
    {
        var test = _items;
    }
}