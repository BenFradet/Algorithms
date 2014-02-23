using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class InsertionSortClass
    {
        public static int[] InsertionSort(int[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                int key = input[i];
                int j = i - 1;
                while (j >= 0 && input[j] > key)
                {
                    input[j + 1] = input[j];
                    j--;
                }
                input[j + 1] = key;
            }
            return input;
        }

        public static float[] InsertionSort(float[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                float key = input[i];
                int j = i - 1;
                while (j >= 0 && input[j] > key)
                {
                    input[j + 1] = input[j];
                    j--;
                }
                input[j + 1] = key;
            }
            return input;
        }
    }
}
