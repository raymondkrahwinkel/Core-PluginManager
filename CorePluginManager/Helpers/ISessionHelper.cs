namespace CorePluginManager.Helpers;

public interface ISessionHelper
{
    bool Exists(string key);
    bool Exists(string group, string key);
    T? Get<T>(string key);
    T? Get<T>(string group, string key);
    void Set(string key, object payload);
    void Set(string group, string key, object payload);
    void Delete(string key);
    void Delete(string group, string key);
}