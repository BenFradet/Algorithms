using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class WeightedGraphAM
    {
        private int[,] weights;
        int maxInt = int.MaxValue / 2 - 200;

        public WeightedGraphAM(int[,] weights)
        {
            this.weights = weights;
        }

        public int[,] FloydWarshall()
        {
            int n = weights.GetLength(0);
            var shortestPaths = new int[n][,];
            var predecessorMatrices = new int?[n][,];
            predecessorMatrices[0] = ComputePredecessorMatrixZero();
            shortestPaths[0] = weights;
            for (int k = 1; k < n; k++)
            {
                shortestPaths[k] = new int[n, n];
                predecessorMatrices[k] = new int?[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        shortestPaths[k][i, j] = 
                            Math.Min(shortestPaths[k - 1][i, j], 
                                shortestPaths[k - 1][i, k] + shortestPaths[k - 1][k, j]);
                        if (shortestPaths[k - 1][i, j] <= shortestPaths[k - 1][i, k] + shortestPaths[k - 1][k, j])
                        {
                            predecessorMatrices[k][i, j] = predecessorMatrices[k - 1][i, j];
                        }
                        else
                        {
                            predecessorMatrices[k][i, j] = predecessorMatrices[k - 1][k, j];
                        }
                    }
                }
            }
            PrintMatrix(shortestPaths[n - 1]);
            PrintMatrix(predecessorMatrices[n - 1]);
            PrintAllPairsShortestPaths(predecessorMatrices[n - 1], 2, 4);
            return shortestPaths[n - 1];
        }

        private int?[,] ComputePredecessorMatrixZero()
        {
            int n = weights.GetLength(0);
            var result = new int?[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (weights[i, j] >= maxInt || i == j) //arbitrary maxbound on weights
                    {
                        result[i, j] = null;
                    }
                    else
                    {
                        result[i, j] = i;
                    }
                }
            }
            return result;
        }

        public int[,] SlowAllPairsShortestPath()
        {
            int n = weights.GetLength(0);
            var l = new int[n][,];
            l[0] = weights;
            for (int m = 1; m < n - 1; m++)
            {
                l[m] = new int[n, n];
                l[m] = ExtendShortestPaths(l[m-1], weights);
            }
            PrintMatrix(l[n - 2]);
            return l[n - 2];
        }

        public int[,] FasterAllPairsShortestPath()
        {
            int n = weights.GetLength(0);
            var l = new int[n][,];
            l[0] = weights;
            int m = 1;
            while (m < n - 1)
            {
                l[2 * m - 1] = new int[n, n];
                l[2 * m - 1] = ExtendShortestPaths(l[m - 1], l[m - 1]);
                m *= 2;
            }
            PrintMatrix(l[m - 1]);
            return l[m - 1];
        }

        private int[,] ExtendShortestPaths(int[,] l, int[,] weights)
        {
            int n = l.GetLength(0);
            var lprim = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    lprim[i, j] = int.MaxValue;
                    for (int k = 0; k < n; k++)
                    {
                        lprim[i, j] = Math.Min(lprim[i, j], l[i, k] + weights[k, j]);
                    }
                }
            }
            return lprim;
        }

        private void PrintAllPairsShortestPaths(int?[,] predecessorMatrix, int i, int j)
        {
            if (i == j)
            {
                Console.WriteLine(i);
            }
            else if (predecessorMatrix[i, j] == null)
            {
                Console.WriteLine("no path from i to j");
            }
            else
            {
                PrintAllPairsShortestPaths(predecessorMatrix, i, predecessorMatrix[i, j].Value);
                Console.WriteLine(j);
            }
        }

        private void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintMatrix(int?[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] == null ? "null " : matrix[i,j] + "    ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
