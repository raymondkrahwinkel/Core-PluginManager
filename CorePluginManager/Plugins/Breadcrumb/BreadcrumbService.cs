using System.Reflection;
using CorePluginManager.Plugins.Breadcrumb.Attributes;
using CorePluginManager.Plugins.Breadcrumb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorePluginManager.Plugins.Breadcrumb;

public class BreadcrumbService : IBreadcrumbService
{
    private readonly List<BreadcrumbItem> _items = new();
    public List<BreadcrumbItem> Items => _items;

    private static BreadcrumbItem DefaultItem = null!;

    public BreadcrumbService()
    {
        if(DefaultItem != null)
            _items.Add(DefaultItem);
    }

    #region Initialization
    /// <summary>
    /// Initialize the breadcrumb plugin
    /// </summary>
    /// <param name="assembly"></param>
    public static void Initialize(Assembly assembly)
    {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (TryGetDefaultBreadcrumbItemFromController(type, out var item))
            {
                DefaultItem = item;
            }
        }
    }
    
    private static bool TryGetDefaultBreadcrumbItemFromController(Type type, out BreadcrumbItem item)
    {
        // check if the type is a MVC controller
        if (typeof(Controller).IsAssignableFrom(type))
        {
            var defaultBreadcrumbAttribute = type.GetCustomAttribute<DefaultBreadcrumbAttribute>();
            if (defaultBreadcrumbAttribute != null)
            {
                EnricDefaulthBreadcrumbItem(defaultBreadcrumbAttribute, type);
                item = defaultBreadcrumbAttribute.Item;
                return true;
            }
            else
            {
                if (TryGetDefaultBreadcrumbItemFromMethods(type, out item))
                {
                    return true;
                }
            }
        }

        // assign output to default null
        item = null!;
        return false;
    }

    private static bool TryGetDefaultBreadcrumbItemFromMethods(Type type, out BreadcrumbItem item)
    {
        // start checking the methods
        foreach (var method in type.GetMethods())
        {
            // check if the type is a MVC action
            if (typeof(IActionResult).IsAssignableFrom(method.ReturnType) || IsGenericActionResult(method.ReturnType))
            {
                var defaultBreadcrumbAttribute = method.GetCustomAttribute<DefaultBreadcrumbAttribute>();
                if (defaultBreadcrumbAttribute != null)
                {
                    EnricDefaulthBreadcrumbItem(defaultBreadcrumbAttribute, type);
                    item = defaultBreadcrumbAttribute.Item;
                    return true;
                }
            }
        }
        
        // assign output to default null
        item = null!;
        return false;
    }

    private static bool IsGenericActionResult(Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
        {
            Type genericType = type.GetGenericArguments()[0];
            return typeof(IActionResult).IsAssignableFrom(genericType);
        }

        return false;
    }

    private static void EnricDefaulthBreadcrumbItem(DefaultBreadcrumbAttribute defaultBreadcrumbAttribute, Type type)
    {
        // check if the controller is bound to a area
        var areaAttribute = type.GetCustomAttribute<AreaAttribute>();
        if (areaAttribute != null)
        {
            defaultBreadcrumbAttribute.Area = areaAttribute.RouteValue;
        }
        
        // add controller information
        defaultBreadcrumbAttribute.Controller = type.Name;

        // sanitize controller name
        if (defaultBreadcrumbAttribute.Controller != "Controller" &&
            defaultBreadcrumbAttribute.Controller.EndsWith("Controller"))
        {
            defaultBreadcrumbAttribute.Controller =
                defaultBreadcrumbAttribute.Controller.Substring(0, defaultBreadcrumbAttribute.Controller.Length - 10);
        }
    }
    #endregion

    public void Add(BreadcrumbItem item)
    {
        if (item.IsDefault && _items.Any(x => x.IsDefault))
        {
            throw new PluginException("Breadcrumb", "there can only be 1 default breadcrumb item");
        }
        
        _items.Insert(1, item);
    }

    public void SetItems(List<BreadcrumbItem> items)
    {
        _items.Clear();
        _items.AddRange(items);
    }
}