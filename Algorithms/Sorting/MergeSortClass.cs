using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class MergeSortClass
    {
        public static void MergeSort(int[] input, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;
                MergeSort(input, l, m);
                MergeSort(input, m + 1, r);
                Merge(input, l, m, r);
            }
        }

        private static void Merge(int[] input, int l, int m, int r)
        {
            int[] tmp = new int[input.Length];
            int i = l;
            int j = m + 1;
            int k = l;

            for (int c = l; c <= r; c++)
            {
                tmp[c] = input[c];
            }

            while (i <= m && j <= r)
            {
                if (tmp[i] < tmp[j])
                {
                    input[k] = tmp[i];
                    i++;
                }
                else
                {
                    input[k] = tmp[j];
                    j++;
                }
                k++;
            }

            while (i <= m)
            {
                input[k] = tmp[i];
                i++;
                k++;
            }

            while (j <= r)
            {
                input[k] = tmp[j];
                j++;
                k++;
            }
        }
    }
}
