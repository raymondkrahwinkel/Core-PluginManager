﻿using System.Reflection;
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
                var i = item;
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
            if (typeof(IActionResult).IsAssignableFrom(method.ReturnType)) // todo: add check for Task<> and then if result type is IActionResult
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

    private static void EnricDefaulthBreadcrumbItem(DefaultBreadcrumbAttribute defaultBreadcrumbAttribute, Type type)
    {
        // check if the controller is bound to a area
        var areaAttribute = type.GetCustomAttribute<AreaAttribute>();
        if (areaAttribute != null)
        {
            defaultBreadcrumbAttribute.Area = areaAttribute.RouteValue;
        }
        
        // add controller information
        // todo: sanitize name (remove Controller when present at the end)
        defaultBreadcrumbAttribute.Controller = type.Name;
    }
    #endregion

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