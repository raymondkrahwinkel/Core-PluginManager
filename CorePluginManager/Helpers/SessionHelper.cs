using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CorePluginManager.Helpers;

public class SessionHelper : ISessionHelper
{
    private readonly ITempDataDictionary _tempData;
    private readonly ILogger<SessionHelper> _logger;

    public SessionHelper(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataFactory, ILogger<SessionHelper> logger)
    {
        _tempData = tempDataFactory.GetTempData(httpContextAccessor.HttpContext);
        _logger = logger;
    }


    public bool Exists(string key)
    {
        return Exists(string.Empty, key);
    }

    public bool Exists(string @group, string key)
    {
        return _tempData.ContainsKey(_BuildKey(@group, key));
    }

    public T? Get<T>(string key)
    {
        return Get<T>(string.Empty, key);
    }

    public T? Get<T>(string @group, string key)
    {
        if (!Exists(@group, key)) return default;
        
        var result = _tempData[_BuildKey(@group, key)]?.ToString();
        if (result != null && !string.IsNullOrEmpty(result))
        {
            try
            {
                return ((JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(result)!).ToObject<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during session helper get action");
            }
        }

        return default;
    }

    public void Set(string key, object payload)
    {
        Set(string.Empty, key, payload);
    }

    public void Set(string @group, string key, object payload)
    {
        _tempData[_BuildKey(group, key)] = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
    }

    public void Delete(string key)
    {
        Delete(string.Empty, key);
    }

    public void Delete(string @group, string key)
    {
        if (Exists(group, key))
        {
            _tempData.Remove(_BuildKey(group, key));
        }
    }
    
    private string _BuildKey(string @group, string key)
    {
        if (!string.IsNullOrEmpty(group))
        {
            return $"cpm-{group}-{key}";
        }

        return $"cpm-default-{key}";
    }
}