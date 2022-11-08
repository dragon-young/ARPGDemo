using System;

namespace Assets.Scripts.Character
{
    class ArrayHelper<T> where T : IComparable
    {
        public static T[] BubbleSort(T[] sums)
        { 
            for (int i = 0; i < sums.Length; i++)
            {
                for (int j = 0; j < sums.Length - i - 1; j++)
                {
                    if (sums[j].CompareTo(sums[j + 1]) > 0)
                    {
                        // 交换 第i个和第i+1的顺序
                        T temp = sums[j];
                        sums[j] = sums[j + 1];
                        sums[j + 1] = temp;
                    }
                }
            }
            return sums;
        }
    }
}
