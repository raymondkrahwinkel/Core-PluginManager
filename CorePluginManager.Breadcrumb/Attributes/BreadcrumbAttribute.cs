using System.Reflection;
using System.Runtime.CompilerServices;
using CorePluginManager.Breadcrumb.Models;
using CorePluginManager.Breadcrumb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CorePluginManager.Breadcrumb.Attributes;

[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true)]
public class BreadcrumbAttribute : ActionFilterAttribute
{
    /// <summary>
    /// The title for this breadcrumb item
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Target controller
    /// </summary>
    public string? Controller { get; set; }

    /// <summary>
    /// Target action
    /// </summary>
    public string? Action { get; set; }
    
    /// <summary>
    /// Target area
    /// </summary>
    public string? Area { get; set; }
    
    /// <summary>
    /// Additional url parameters
    /// </summary>
    public object? Parameters { get; set; }

    /// <summary>
    /// The default breadcrumb item
    /// </summary>
    public virtual bool IsDefault => false;

    public BreadcrumbAttribute()
    {
        Title = string.Empty;
    }
    
    public BreadcrumbAttribute(string title, [CallerMemberName] string memberName = "")
    {
        if (string.IsNullOrEmpty(memberName))
        {
            Action = PluginManager.GetPluginManagerOptions<BreadcrumbOptions>(new())?.DefaultAction;
        }
        
        Title = title;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var controller = context.Controller as Controller;
        
        // get area information
        var areaAttribute = controller?.GetType().GetCustomAttribute<AreaAttribute>();
        if (areaAttribute != null)
        {
            Area = areaAttribute.RouteValue;
        }
        
        // when no controller is given, lookup
        // if (string.IsNullOrEmpty(Controller) && context.HttpContext.Request.RouteValues.TryGetValue("controller", out var controllerName))
        if (string.IsNullOrEmpty(Controller) && controller != null &&!string.IsNullOrEmpty(controller?.ControllerContext.ActionDescriptor.ControllerName))
        {
            Controller = controller.ControllerContext.ActionDescriptor.ControllerName;
        }
        
        // when no action is given, lookup
        // if (string.IsNullOrEmpty(Action) && context.HttpContext.Request.RouteValues.TryGetValue("action", out var actionName))
        if (string.IsNullOrEmpty(Action) && controller != null && !string.IsNullOrEmpty(controller?.ControllerContext.ActionDescriptor.ActionName))
        {
            Action = controller.ControllerContext.ActionDescriptor.ActionName;
        }
        
        var breadcrumbService = context.HttpContext.RequestServices.GetRequiredService<IBreadcrumbService>();
        breadcrumbService.Add(ToItem());
        
        base.OnActionExecuted(context);
    }

    protected BreadcrumbItem ToItem()
    {
        return new()
        {
            Title = Title,
            Action = Action,
            Area = Area,
            Controller = Controller,
            Parameters = Parameters,
            IsDefault = IsDefault
        };
    }
}