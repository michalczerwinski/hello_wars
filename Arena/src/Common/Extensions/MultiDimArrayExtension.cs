using System;

namespace Common.Extensions
{
    public static class MultiDimArrayExtension
    {
        public static void ForEveryElement<T>(this T[,] array, Action<int, int, T> action)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    action(i, j, array[i, j]);
                }
            }
        }
    }
}
