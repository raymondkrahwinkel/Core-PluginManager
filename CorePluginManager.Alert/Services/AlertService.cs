using CorePluginManager.Helpers;
using Microsoft.Extensions.Logging;

namespace CorePluginManager.Alert.Services;

public class AlertService : IAlertService
{
    private const string SessionGroup = "alert";
    
    private readonly ISessionHelper _sessionHelper;
    private readonly ILogger<AlertService> _logger;

    public AlertService(ISessionHelper sessionHelper, ILogger<AlertService> logger)
    {
        _sessionHelper = sessionHelper;
        _logger = logger;
    }
    
    /// <summary>
    /// add a new error messsage
    /// </summary>
    public void Error(string message)
    {
        Add("error", message);
    }

    /// <summary>
    /// add a new success messsage
    /// </summary>
    public void Success(string message)
    {
        Add("success", message);
    }

    /// <summary>
    /// add a new warning messsage
    /// </summary>
    public void Warning(string message)
    {
        Add("warning", message);
    }

    /// <summary>
    /// add a new info messsage
    /// </summary>
    public void Info(string message)
    {
        Add("info", message);
    }
    
    /// <summary>
    /// get list with all error messages
    /// </summary>
    public List<string> GetErrorMessages()
    {
        return Get("error");
    }

    /// <summary>
    /// get list with all success messages
    /// </summary>
    public List<string> GetSuccessMessages()
    {
        return Get("success");
    }

    /// <summary>
    /// get list with all warning messages
    /// </summary>
    public List<string> GetWarningMessages()
    {
        return Get("warning");
    }

    /// <summary>
    /// get list with all info messages
    /// </summary>
    public List<string> GetInfoMessages()
    {
        return Get("info");
    }

    /// <summary>
    /// add a message to the temporary storage
    /// </summary>
    /// <param name="type"></param>
    /// <param name="message"></param>
    private void Add(string type, string message)
    {
        List<string> messageList = _sessionHelper.Get<List<string>>(SessionGroup, type) ?? new List<string>();
        if (!messageList.Contains(message))
        {
            messageList.Add(message);
            _sessionHelper.Set(SessionGroup, type, messageList);
        }
    }

    /// <summary>
    /// Get list with message for certain type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private List<string> Get(string type)
    {
        try
        {
            var messages = _sessionHelper.Get<List<string>>(SessionGroup, type) ?? new List<string>();
            _sessionHelper.Delete(SessionGroup, type);
            return messages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during retrieval of alert messages");
        }

        return new List<string>();
    }
}