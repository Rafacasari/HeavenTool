using System;
using System.Collections.Generic;
using System.Linq;

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

        public static T? FirstOrNull<T>(this IEnumerable<T> sequence, Func<T, bool> predicate) where T : struct
        {
            if (sequence == null || predicate == null)
                return null;

            foreach (T item in sequence.Where(predicate))
                return item;
            return null;
        }
    }
}
