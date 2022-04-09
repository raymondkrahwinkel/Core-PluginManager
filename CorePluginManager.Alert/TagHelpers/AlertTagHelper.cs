using CorePluginManager.Alert.Models;
using CorePluginManager.Alert.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorePluginManager.Alert.TagHelpers;

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
        var options = PluginManager.GetPluginManagerOptions<AlertOptions>(new());
        
        var child = await output.GetChildContentAsync();

        var errorMessages = _alertService.GetErrorMessages();
        if (errorMessages.Any())
        {
            _GenerateAlert(output.Content, errorMessages, options, options.ErrorMessages);
        }
        
        
        var infoMessages = _alertService.GetInfoMessages();
        if (infoMessages.Any())
        {
            _GenerateAlert(output.Content, infoMessages, options, options.InfoMessages);
        }
        
        var warningMessages = _alertService.GetWarningMessages();
        if (warningMessages.Any())
        {
            _GenerateAlert(output.Content, warningMessages, options, options.WarningMessages);
        }
        
        var successMessage = _alertService.GetSuccessMessages();
        if (successMessage.Any())
        {
            _GenerateAlert(output.Content, successMessage, options, options.SuccessMessages);
        }
        
        // place the extracted child html below the alert
        output.Content.AppendHtml(child);
    }

    private void _GenerateAlert(TagHelperContent content, List<string> messages, AlertOptions options, AlertOptions.MessageOptions messageOptions)
    {
        string cssClass = messageOptions.CssClass;
        if (messageOptions.Dismissible && !string.IsNullOrEmpty(options.DismissibleCssClass))
        {
            cssClass = $"{cssClass} {options.DismissibleCssClass}";
        }
        
        content.AppendHtml($"<div class=\"alert {cssClass}\" role=\"alert\">");
        content.AppendHtml(string.Join("<br />", messages.ToArray()));
        if (messageOptions.Dismissible)
        {
            content.AppendHtml("<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>");
        }
        content.AppendHtml("</div>");
    }
}