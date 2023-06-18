using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace cwu.cs.TaAssignments
{
    static class Extensions
    {
        public static bool Matches(this string str, string pattern)
        {
            return Regex.IsMatch(str, "^" + pattern + "$");
        }

        public static T[] ToArray<T>(this ICollection<T> set)
        {
            T[] arr = new T[set.Count];
            set.CopyTo(arr, 0);
            return arr;
        }
    }
}
