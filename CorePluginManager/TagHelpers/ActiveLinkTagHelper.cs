using CorePluginManager.Enums;
using CorePluginManager.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorePluginManager.TagHelpers;

[HtmlTargetElement()]
public class ActiveLinkTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    [ViewContext] 
    [HtmlAttributeNotBound] 
    public ViewContext ViewContext { get; set; } = null!;

    [HtmlAttributeName("active-link-controller")]
    public string Controller { get; set; } = "";

    [HtmlAttributeName("active-link-action")]
    public string Action { get; set; } = "";

    [HtmlAttributeName("active-link-area")]
    public string? Area { get; set; } = null;
    
    [HtmlAttributeName("active-link-all-route-data", DictionaryAttributePrefix = "active-link-route-")]
    public Dictionary<string, object> Data { get; set; } = new();

    [HtmlAttributeName("active-link-css-class-action")]
    public ActiveLinkCssClassAction? CssClassAction { get; set; } = null;

    [HtmlAttributeName("active-link-css-class-active")]
    public string? OverrideActiveCssClass { get; set; } = null;
    
    public ActiveLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var options = PluginManager.GetPluginManagerOptions<ActiveLinkOptions>(new());
        
        // todo: add option/override htmlattr for exact routedata match / only area+controller+action 
        // todo: add aria-current support via configuration
        string defaultAction = options.DefaultAction;
        string activeClass = options.DefaultActiveCssClass;
        ActiveLinkCssClassAction cssClassAction = CssClassAction ?? options.DefaultCssAction;

        if (!string.IsNullOrEmpty(OverrideActiveCssClass))
        {
            activeClass = OverrideActiveCssClass;
        }
        
        if (!string.IsNullOrEmpty(Area) || !string.IsNullOrEmpty(Controller) || !string.IsNullOrEmpty(Action))
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var parameters = Data;
            parameters.Add("Area", Area ?? "");

            // when it is an A tag, insert the url
            if (context.TagName.Equals("a", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(Controller) && !string.IsNullOrEmpty(Action))
            {
                if (!output.Attributes.ContainsName("href") || (output.Attributes["href"].Value.ToString() == string.Empty || output.Attributes["href"].Value.ToString() == "#"))
                {
                    var url = urlHelper.Action(Action, Controller, parameters);
                    output.Attributes.SetAttribute("href", url);                    
                }
            }
            
            // check if this is the active route
            string currentArea = string.Empty;
            if (ViewContext.HttpContext.Request.RouteValues.TryGetValue("Area", out var areaValue))
            {
                currentArea = areaValue?.ToString() ?? "";
            }
            
            if(ViewContext.HttpContext.Request.RouteValues.TryGetValue("Controller", out var controllerValue) && !string.IsNullOrEmpty(controllerValue as string))
            {
                string currentAction = defaultAction;
                if (ViewContext.HttpContext.Request.RouteValues.TryGetValue("Action", out var actionValue) && !string.IsNullOrEmpty(actionValue as string))
                {
                    currentAction = actionValue.ToString() ?? defaultAction;
                }

                if (
                    (
                     // (string.IsNullOrEmpty(currentArea) && string.IsNullOrEmpty(Area))
                     // &&
                     (
                         currentArea.Equals(Area, StringComparison.OrdinalIgnoreCase)) && 
                     currentAction!.Equals(Action, StringComparison.OrdinalIgnoreCase) && 
                     ((controllerValue as string)!).Equals(Controller, StringComparison.OrdinalIgnoreCase)
                    )
                    ||
                    (
                        !string.IsNullOrEmpty(Area) && string.IsNullOrEmpty(Controller) && string.IsNullOrEmpty(Action) && currentArea.Equals(Area, StringComparison.OrdinalIgnoreCase)
                    )
                    ||
                    (
                        !string.IsNullOrEmpty(Area) && !string.IsNullOrEmpty(Controller) && string.IsNullOrEmpty(Action) && currentArea.Equals(Area, StringComparison.OrdinalIgnoreCase) && ((controllerValue as string)!).Equals(Controller, StringComparison.OrdinalIgnoreCase)
                    )
                )
                {
                    string cssClassString = "";
                    if (output.Attributes.ContainsName("class"))
                    {
                        string? currentClassValue = output.Attributes["class"].Value.ToString();

                        if (cssClassAction == ActiveLinkCssClassAction.Append)
                        {
                            cssClassString = $"{currentClassValue} {activeClass}";
                        }
                        else
                        {
                            cssClassString = activeClass;
                        }
                    }
                    else
                    {
                        cssClassString = activeClass;
                    }
                    
                    output.Attributes.SetAttribute("class", cssClassString);
                }
            }
        }
    }
}