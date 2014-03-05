using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class GraphAM
    {
        bool[,] edges;

        public GraphAM(bool[,] edges)
        {
            this.edges = edges;
        }

        public bool[,] TransitiveClosure()
        {
            int n = edges.GetLength(0);
            var closures = new bool[n][,];
            closures[0] = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j || edges[i, j])
                    {
                        closures[0][i, j] = true;
                    }
                    else
                    {
                        closures[0][i, j] = false;
                    }
                }
            }
            for (int k = 1; k < n; k++)
            {
                closures[k] = new bool[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        closures[k][i, j] = closures[k - 1][i, j] || (closures[k - 1][i, k] && closures[k - 1][k, j]);
                    }
                }
            }
            PrintMatrix(closures[n - 1]);
            return closures[n - 1];
        }

        private void PrintMatrix(bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write((matrix[i, j] ? "1" : "0") + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
