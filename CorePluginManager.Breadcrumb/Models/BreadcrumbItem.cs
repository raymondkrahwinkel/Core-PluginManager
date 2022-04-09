namespace CorePluginManager.Breadcrumb.Models;

public class BreadcrumbItem
{
    /// <summary>
    /// Title to use in the breadcrumb item
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Target area for the breadcrumb item
    /// </summary>
    public string? Area { get; set; }

    /// <summary>
    /// Target controller for the breadcrumb item
    /// </summary>
    public string? Controller { get; set; }
    
    /// <summary>
    /// Target action for the breadcrumb item
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Additional parameters for the url of the breadcrumb item
    /// </summary>
    public object? Parameters { get; set; }

    /// <summary>
    /// Marks the default breadcrumb item (max one)
    /// </summary>
    public bool IsDefault { get; set; } = false;
}