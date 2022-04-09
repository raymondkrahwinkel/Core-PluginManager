namespace CorePluginManager.Alert.Models;

public class AlertOptions
{
    /// <summary>
    /// Css class to use for dismissible
    /// </summary>
    public string DismissibleCssClass { get; set; } = "alert-dismissible fade show";
    
    /// <summary>
    /// Error message options
    /// </summary>
    public MessageOptions ErrorMessages { get; set; } = new()
    {
        CssClass = "alert-danger"
    };
    
    /// <summary>
    /// Warning message options
    /// </summary>
    public MessageOptions WarningMessages { get; set; } = new()
    {
        CssClass = "alert-warning"
    };
    
    /// <summary>
    /// Success message options
    /// </summary>
    public MessageOptions SuccessMessages { get; set; } = new()
    {
        CssClass = "alert-success"
    };
    
    /// <summary>
    /// Info message options
    /// </summary>
    public MessageOptions InfoMessages { get; set; } = new()
    {
        CssClass = "alert-info"
    };
    
    public class MessageOptions
    {
        /// <summary>
        /// Css class for the alert box
        /// </summary>
        public string CssClass { get; set; }
        
        /// <summary>
        /// Messages are dismissible
        /// </summary>
        public bool Dismissible { get; set; } = true;
    }
}