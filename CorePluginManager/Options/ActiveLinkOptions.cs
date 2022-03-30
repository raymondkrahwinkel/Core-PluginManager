using CorePluginManager.Enums;

namespace CorePluginManager.Options;

public class ActiveLinkOptions
{
    /// <summary>
    /// Default action to use
    /// </summary>
    public string DefaultAction { get; set; } = "Index";

    /// <summary>
    /// Default active css class
    /// </summary>
    public string DefaultActiveCssClass { get; set; } = "active";

    /// <summary>
    /// Default action for class replace/append
    /// </summary>
    public ActiveLinkCssClassAction DefaultCssAction { get; set; } = ActiveLinkCssClassAction.Append;
}