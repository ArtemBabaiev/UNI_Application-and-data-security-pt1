using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class ArrayOperations
    {
        public static IList<T> Shift<T>(IList<T> arr, int n)
        {
            while (n > 0)
            {
                T temp = arr[0];
                for (int i = 0; i < arr.Count - 1; i++)
                {
                    arr[i] = arr[i + 1];
                }
                arr[arr.Count - 1] = temp;
                n--;
            }
            return arr;
        }

        public static string EnumerableToString<T>(IEnumerable<T> arr)
        {
            StringBuilder sb = new();
            foreach (T item in arr)
            {
                sb.Append(item?.ToString());
            }
            return sb.ToString();
        }
    }
}
