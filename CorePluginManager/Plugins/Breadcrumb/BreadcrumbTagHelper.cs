using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorePluginManager.Plugins.Breadcrumb;

[HtmlTargetElement("breadcrumb")]
public class BreadcrumbTagHelper : TagHelper
{
    private readonly IBreadcrumbService _breadcrumbService;

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public BreadcrumbTagHelper(IBreadcrumbService breadcrumbService)
    {
        _breadcrumbService = breadcrumbService;
    }
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var child = await output.GetChildContentAsync();

        output.Content.AppendHtml("<nav aria-label=\"breadcrumb\">");
        output.Content.AppendHtml("<ol class=\"breadcrumb\">");
        
        output.Content.AppendHtml("</ol>");
        output.Content.AppendHtml("</nav>");
        
        // place the extracted child html below the alert
        output.Content.AppendHtml(child);
    }
}