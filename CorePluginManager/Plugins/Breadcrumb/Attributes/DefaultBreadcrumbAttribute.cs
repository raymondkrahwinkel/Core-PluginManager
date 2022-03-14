using Microsoft.AspNetCore.Mvc.Filters;

namespace CorePluginManager.Plugins.Breadcrumb.Attributes;

[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
public class DefaultBreadcrumbAttribute : BreadcrumbAttribute
{
    public override bool IsDefault => true;
    
    public DefaultBreadcrumbAttribute() : base() { }

    public DefaultBreadcrumbAttribute(string title) : base(title) { }

    public override void OnActionExecuted(ActionExecutedContext context) { }
}