using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorePluginManager.Plugins.Alert;

[HtmlTargetElement("alert")]
public class AlertTagHelper : TagHelper
{
    private readonly IAlertService _alertService;
    
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public AlertTagHelper(IAlertService alertService)
    {
        _alertService = alertService;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var child = await output.GetChildContentAsync();

        var errorMessages = _alertService.GetErrorMessages();
        if (errorMessages.Any())
        {
            _GenerateAlert(output.Content, errorMessages, "alert-danger");
        }
        
        
        var infoMessages = _alertService.GetInfoMessages();
        if (infoMessages.Any())
        {
            _GenerateAlert(output.Content, infoMessages, "alert-primary");
        }
        
        var warningMessages = _alertService.GetWarningMessages();
        if (warningMessages.Any())
        {
            _GenerateAlert(output.Content, warningMessages, "alert-warning");
        }
        
        var successMessage = _alertService.GetSuccessMessages();
        if (successMessage.Any())
        {
            _GenerateAlert(output.Content, successMessage, "alert-success");
        }
        
        // place the extracted child html below the alert
        output.Content.AppendHtml(child);
    }

    private void _GenerateAlert(TagHelperContent content, List<string> messages, string className)
    {
        content.AppendHtml($"<div class=\"alert {className}\" role=\"alert\">");
        content.AppendHtml(string.Join("<br />", messages.ToArray()));
        content.AppendHtml("</div>");
    }
}