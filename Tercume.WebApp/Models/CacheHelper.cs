using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Tercume.BusinessLayer;
using Tercume.Entities;

namespace Tercume.WebApp.Models
{
    public class CacheHelper
    {
        public static List<Dil> GetLanguageFromCache()
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                DilManager dilManager = new DilManager();
                result = dilManager.List();

                WebCache.Set("Language-cache", result, 20, true);
            }

            return result;
        }

        public static void RemoveCategoriesFromCache()
        {
            Remove("Language-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}