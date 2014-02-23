using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class QuickSortClass
    {
        public static void RandomizedQuickSort(int[] input, int p, int r)
        {
            if (p < r)
            {
                int q = RandomizedPartition(input, p, r);
                RandomizedQuickSort(input, p, q - 1);
                RandomizedQuickSort(input, q + 1, r);
            }
        }

        public static int RandomizedSelect(int[] input, int p, int r, int index)
        {
            if (p == r)
                return input[p];
            int q = RandomizedPartition(input, p, r);
            int k = q - p + 1;
            if (index == k)
                return input[q];
            else if (index < k)
                return RandomizedSelect(input, p, q - 1, index);
            else
                return RandomizedSelect(input, q + 1, r, index - k);
        }

        //public static void QuickSortSimple(int[] input, int left, int right)
        //{
        //    int i = left, j = right;
        //    int pivot = input[(left + right) / 2];

        //    while (i <= j)
        //    {
        //        while (input[i] < pivot)
        //            i++;
        //        while (input[j] > pivot)
        //            j--;
        //        if (i <= j)
        //        {
        //            int tmp = input[i];
        //            input[i] = input[j];
        //            input[j] = tmp;
        //            i++;
        //            j--;
        //        }
        //    }

        //    if (left < j)
        //        QuickSortSimple(input, left, j);
        //    if (i < right)
        //        QuickSortSimple(input, i, right);
        //}

        public static void QuickSort(int[] input, int p, int r)
        {
            if (p < r)
            {
                int q = Partition(input, p, r);
                QuickSort(input, p, q - 1);
                QuickSort(input, q + 1, r);
            }
        }

        private static int RandomizedPartition(int[] input, int p, int r)
        {
            var rand = new Random();
            int i = rand.Next(p, r);
            int tmp = input[i];
            input[i] = input[r];
            input[r] = tmp;
            return Partition(input, p, r);
        }

        //private static int IndexedPartition(int[] input, int p, int r, int index)
        //{
        //    int tmp = input[index];
        //    input[index] = input[input.Length - 1];
        //    input[input.Length - 1] = tmp;
        //    return Partition(input, p, r);
        //}

        private static int Partition(int[] input, int p, int r)
        {
            int x = input[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (input[j] <= x)
                {
                    i++;
                    int tmp = input[i];
                    input[i] = input[j];
                    input[j] = tmp;
                }
            }
            int tmp2 = input[i + 1];
            input[i + 1] = input[r];
            input[r] = tmp2;
            return i + 1;
        }
    }
}
