using System;
using System.Linq;
using System.Collections.Generic;

namespace Deanony.Metrics
{
    static class ContentExtraction
    {
        public static string[] Words(string data)
        {
            while (data.Contains("  "))
            {
                data = data.Replace("  ", " ");
            }

            return data.Split(' ');
        }

        public static string[] Sentences(string data)
        {
            char[] splits = { '.', '!', '?' };
            return data.Split(splits);
        }

        // Words() sans duplicates
        public static string[] WordBag(string data)
        {
            return Words(data).Distinct().ToArray();
        }
    }
}
