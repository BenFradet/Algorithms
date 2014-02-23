using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class SelectionSortClass
    {
        public static int[] SelectionSort(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int min = i;
                for (int j = i; j < input.Length; j++)
                {
                    if (input[j] < input[min])
                        min = j;
                }
                int tmp = input[i];
                input[i] = input[min];
                input[min] = tmp;
            }
            return input;
        }
    }
}
