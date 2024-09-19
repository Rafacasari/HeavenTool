using System;
using System.Collections.Generic;

namespace HeavenTool.Utility
{
    public static class ListUtilities
    {
        public static void AddIfNotExist<T>(this List<T> list, T key) where T : class
        {
            if (list == null) throw new ArgumentNullException("list");

            if (key == null) throw new ArgumentNullException("key");

            if (list.Contains(key)) return;

            list.Add(key);
        }
    }
}
