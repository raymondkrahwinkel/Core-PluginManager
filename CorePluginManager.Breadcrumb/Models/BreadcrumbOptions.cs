namespace CorePluginManager.Breadcrumb.Models;

public class BreadcrumbOptions
{
    /// <summary>
    /// The default action to use
    /// </summary>
    public string DefaultAction { get; set; } = "Index";
    
    /// <summary>
    /// Active css class
    /// </summary>
    public string ActiveCssClass { get; set; } = "active";
    
    /// <summary>
    /// Breadcrumb ol css class
    /// </summary>
    public string OlCssClass { get; set; } = "breadcrumb";
    
    /// <summary>
    /// Breadcrumb li css class
    /// </summary>
    public string LiCssClass { get; set; } = "breadcrumb-item";
}