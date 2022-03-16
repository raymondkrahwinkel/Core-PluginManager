namespace CorePluginManager.Helpers;

public interface ISessionHelper
{
    /// <summary>
    /// check if a certain key exists within the session
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Exists(string key);
    
    /// <summary>
    /// check if a certain key exists within the session group
    /// </summary>
    /// <param name="group"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Exists(string group, string key);
    
    /// <summary>
    /// get data from the session
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? Get<T>(string key);
    
    /// <summary>
    /// get data from the session within the session group
    /// </summary>
    /// <param name="group"></param>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? Get<T>(string group, string key);
    
    /// <summary>
    /// set data to the session
    /// </summary>
    /// <param name="key"></param>
    /// <param name="payload"></param>
    void Set(string key, object payload);
    
    /// <summary>
    /// set data to the session within a session group
    /// </summary>
    /// <param name="group"></param>
    /// <param name="key"></param>
    /// <param name="payload"></param>
    void Set(string group, string key, object payload);
    
    /// <summary>
    /// delete data from the session
    /// </summary>
    /// <param name="key"></param>
    void Delete(string key);
    
    /// <summary>
    /// delete data from the session within a session group
    /// </summary>
    /// <param name="group"></param>
    /// <param name="key"></param>
    void Delete(string group, string key);
}