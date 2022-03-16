namespace CorePluginManager.Plugins.Alert;

public interface IAlertService
{
    /// <summary>
    /// add a new error messsage
    /// </summary>
    void Error(string message);

    /// <summary>
    /// add a new success messsage
    /// </summary>
    void Success(string message);

    /// <summary>
    /// add a new warning messsage
    /// </summary>
    void Warning(string message);

    /// <summary>
    /// add a new info messsage
    /// </summary>
    void Info(string message);

    /// <summary>
    /// get list with all error messages
    /// </summary>
    List<string> GetErrorMessages();

    /// <summary>
    /// get list with all success messages
    /// </summary>
    List<string> GetSuccessMessages();

    /// <summary>
    /// get list with all warning messages
    /// </summary>
    List<string> GetWarningMessages();

    /// <summary>
    /// get list with all info messages
    /// </summary>
    List<string> GetInfoMessages();
}