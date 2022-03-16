using CorePluginManager.Plugins.Breadcrumb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorePluginManager.Plugins.Breadcrumb;

[HtmlTargetElement("breadcrumb")]
public class BreadcrumbTagHelper : TagHelper
{
    private readonly IBreadcrumbService _breadcrumbService;
    private readonly IUrlHelperFactory _urlHelperFactory;

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public BreadcrumbTagHelper(IBreadcrumbService breadcrumbService, IUrlHelperFactory urlHelperFactory)
    {
        _breadcrumbService = breadcrumbService;
        _urlHelperFactory = urlHelperFactory;
    }
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var options = PluginManager.GetPluginManagerOptions<BreadcrumbOptions>(new());
        
        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
        var child = await output.GetChildContentAsync();

        output.Content.AppendHtml("<nav aria-label=\"breadcrumb\">");
        output.Content.AppendHtml($"<ol class=\"{options.OlCssClass}\">");

        foreach (var item in _breadcrumbService.Items)
        {
            if (item == _breadcrumbService.Items.Last())
            {
                output.Content.AppendHtml($"<li class=\"{options.LiCssClass} {options.ActiveCssClass}\" aria-current=\"page\">{item.Title}</li>");
            }
            else
            {
                var parameters = item.Parameters?.ToDictionary() ?? new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(item.Area))
                {
                    parameters.Add("Area", item.Area);
                }
                
                var url = urlHelper.Action(item.Action ?? options.DefaultAction, item.Controller, parameters);
                output.Content.AppendHtml($"<li class=\"{options.LiCssClass}\"><a href=\"{url}\">{item.Title}</a></li>");   
            }
        }
        
        output.Content.AppendHtml("</ol>");
        output.Content.AppendHtml("</nav>");
        
        // place the extracted child html below the alert
        output.Content.AppendHtml(child);
    }
}