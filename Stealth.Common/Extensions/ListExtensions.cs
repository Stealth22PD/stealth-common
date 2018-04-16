using System.Collections.Generic;

namespace Stealth.Common.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> ShuffledList = new List<T>(list);
            int n = ShuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = EntryPoint.gRandom.Next(n + 1);
                T value = ShuffledList[k];
                ShuffledList[k] = ShuffledList[n];
                ShuffledList[n] = value;
            }

            return ShuffledList;
        }
    }
}