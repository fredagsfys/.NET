using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Easyfy.CHS.Helpers
{
  public class CacheHelper
  {
    public CacheHelper()
    {

    }

    public static void RemoveAllFromCache(string group)
    {

      List<string> keysToRemove = new List<string>();
      var enumerator = HttpContext.Current.Cache.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Key.ToString().StartsWith(group + ":"))
        {
          keysToRemove.Add(enumerator.Key.ToString());
        }

        foreach (var key in keysToRemove)
        {
          HttpContext.Current.Cache.Remove(key);
        }
      }
    }

    public static void RemoveFromCache(string key)
    {
      string keyToRemove = "";
      var enumerator = HttpContext.Current.Cache.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Key.ToString().EndsWith(":" + key))
        {
          keyToRemove = enumerator.Key.ToString();
          break;
        }
        if (String.IsNullOrEmpty(keyToRemove))
          HttpContext.Current.Cache.Remove(key);
      }
    }

    public static void RemoveFromCache(string group, string key)
    {
      if (System.Web.HttpContext.Current.Cache.Get(group + ":" + key) != null)
        System.Web.HttpContext.Current.Cache.Remove(group + ":" + key);
    }

    public static void InsertToCache(string group, string key, int duration, object objectToCache)
    {
      HttpContext.Current.Cache.Insert(group + ":" + key, objectToCache, null, DateTime.Now.AddMinutes(duration),
                                       TimeSpan.Zero);
    }

    public static void InsertToCache(string group, string key, object objectToCache)
    {
      InsertToCache(group, key, 30, objectToCache);
    }

    public static void InsertToCache(string key, object objectToCache)
    {
      InsertToCache("default", key, 30, objectToCache);
    }

    public static void InsertToCache(string key, int duration, object objectToCache)
    {
      InsertToCache("default", key, duration, objectToCache);
    }

    public static T Cacheify<T>(string key, int duration, Func<T> func)
    {
      return Cacheify("default", key, duration, func);
    }

    public static T Cacheify<T>(string key, Func<T> func)
    {
      return Cacheify("default", key, 30, func);
    }

    public static T Cacheify<T>(string group, string key, Func<T> func)
    {
      return Cacheify(group, key, 30, func);
    }

    public static T Cacheify<T>(string group, string key, int duration, Func<T> func)
    {
      if (System.Web.HttpContext.Current.Cache[group + ":" + key] == null)
        System.Web.HttpContext.Current.Cache.Add(group + ":" + key, func.Invoke(), null, DateTime.Now.AddSeconds(duration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

      return (T)System.Web.HttpContext.Current.Cache[group + ":" + key];
    }
  }
}