using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class CountingSortClass
    {
        public static int[] CountingSort(int[] A, int k)
        {
            int[] C = new int[k];
            for (int i = 0; i < A.Length; i++)
            {
                C[A[i]]++;
            }
            for (int i = 1; i < k; i++)
            {
                C[i] += C[i - 1];
            }
            int[] B = new int[A.Length];
            for (int i = A.Length - 1; i >= 0; i--)
            {
                B[--C[A[i]]] = A[i];
            }
            return B;
        }
    }
}
