using System;
using System.Configuration;
using System.Web;
using System.Web.Caching;
public static class CacheHelper
{
    static readonly int _cacheDuration = int.Parse(ConfigurationManager.AppSettings["CacheDuration"] ?? "480"); // 480 min is 8hr

    /// <summary>
    ///		Return Cache or fill it with data from the func
    /// </summary>
    public static T GetCachedData<T>(ref string cacheName, Func<T> selectAllMethod) where T : class
    {
        return GetCachedData(ref cacheName, selectAllMethod, _cacheDuration);
    }

    /// <summary>
    ///		Return Cache or fill it with data from the func
    /// </summary>

    public static T GetCachedData<T>(ref string cacheName, Func<T> selectAllMethod, int cacheDuration) where T : class
    {
        var data = (T)HttpRuntime.Cache[cacheName];

        if (data != null) return data;
        lock (cacheName)
        {
            data = (T)HttpRuntime.Cache[cacheName];
            if (data != null) return data;
            data = selectAllMethod();
            DateTime expiration = DateTime.Now.AddMinutes(cacheDuration);
            HttpRuntime.Cache.Insert(cacheName, data,null,  expiration, Cache.NoSlidingExpiration);
        }
        return data;
    }
    public static T UpdateCachedData<T>(ref string cacheName, T dataToCache) where T : class
    {
        return UpdateCachedData(ref cacheName,  dataToCache, _cacheDuration);
    }
    public static T UpdateCachedData<T>(ref string cacheName, T dataToCache, int cacheDuration) where T : class
    {
        var data = (T)HttpRuntime.Cache[cacheName];

        if (data != null) return data;
        lock (cacheName)
        {
            HttpRuntime.Cache.Remove(cacheName);
            data = dataToCache;
           
            DateTime expiration = DateTime.Now.AddMinutes(cacheDuration);
            HttpRuntime.Cache.Insert(cacheName, data, null, expiration, Cache.NoSlidingExpiration);
        }
        return data;
    }





  
}