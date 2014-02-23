using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class MatrixChainMult
    {
        private int[] dimensions;
        private const int MAX_SIZE = 10;

        public MatrixChainMult()
        {
            dimensions = new int[] { 30, 35, 15, 5, 10, 20, 25};
        }

        public struct MultsAndOptimalCosts
        {
            public int[,] Mults { get; set; }
            public int[,] OptimalCosts { get; set; }
        }

        //return the min number of scalar mults for multiplying every matrice
        public int MemoizedMatrixChain()
        {
            int n = dimensions.Length - 1;
            var costs = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    costs[i, j] = int.MaxValue;
                }
            }
            return LookupChain(costs, 0, n - 1);
        }

        private int LookupChain(int[,] costs, int i, int j)
        {
            if (costs[i, j] < int.MaxValue)
            {
                return costs[i, j];
            }
            if (i == j)
            {
                costs[i, j] = 0;
            }
            else
            {
                for (int k = i; k < j; k++)
                {
                    int candidateMin = LookupChain(costs, i, k) + LookupChain(costs, k + 1, j)
                        + dimensions[i] * dimensions[k + 1] * dimensions[j + 1];
                    if (candidateMin < costs[i, j])
                    {
                        costs[i, j] = candidateMin;
                    }
                }
            }
            return costs[i, j];
        }

        public MultsAndOptimalCosts MatrixChainOrder()
        {
            int n = dimensions.Length - 1;
            MultsAndOptimalCosts result =
                new MultsAndOptimalCosts()
                {
                    Mults = new int[n, n],
                    OptimalCosts = new int[n, n]
                };
            for (int l = 1; l < n; l++)
            {
                for (int i = 0; i < n - l; i++)
                {
                    int j = i + l;
                    result.Mults[i, j] = int.MaxValue;
                    for (int k = i; k < j; k++)
                    {
                        int candidateMin = result.Mults[i, k] + result.Mults[k + 1, j]
                            + dimensions[i] * dimensions[k + 1] * dimensions[j + 1];
                        if (candidateMin < result.Mults[i, j])
                        {
                            result.Mults[i, j] = candidateMin;
                            result.OptimalCosts[i, j] = k;
                        }
                    }
                }
            }

            //for (int l = 2; l <= n; l++)
            //{
            //    for (int i = 1; i <= n - l + 1; i++)
            //    {
            //        int j = i + l - 1;
            //        result.Mults[i, j] = int.MaxValue;
            //        for (int k = i; k <= j - 1; k++)
            //        {
            //            int minMult = result.Mults[i, k] + result.Mults[k + 1, j]
            //                + dimensions[i - 1] * dimensions[k] * dimensions[j];
            //            if (minMult < result.Mults[i, j])
            //            {
            //                result.Mults[i, j] = minMult;
            //                result.OptimalCosts[i, j] = k;
            //            }
            //        }
            //    }
            //}
            return result;
        }

        public void PrintOptimal(int[,] optimalCosts, int i, int j)
        {
            if (i == j)
            {
                Console.WriteLine("A" + i);
            }
            else
            {
                PrintOptimal(optimalCosts, i, optimalCosts[i, j]);
                PrintOptimal(optimalCosts, optimalCosts[i, j] + 1, j);
                Console.WriteLine(")");
            }
        }

        public static int[,] MatrixMult(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
                throw new Exception("incompatible dimensions");
            else
            {
                int[,] c = new int[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.GetLength(1); k++)
                        {
                            c[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }
                return c;
            }
        }
    }
}
