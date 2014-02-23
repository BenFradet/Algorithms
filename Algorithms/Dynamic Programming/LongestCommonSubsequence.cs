using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class LongestCommonSubsequence<T>
    {
        private T[] a;
        private T[] b;

        private Direction[,] directions;
        private int[,] result;

        public enum Direction
        {
            TopLeft,
            Left,
            Top
        }

        public LongestCommonSubsequence(T[] a, T[] b)
        {
            this.a = a;
            this.b = b;
        }

        public struct DirectionAndResultingMatrix
        {
            public Direction[,] Directions { get; set; }
            public int[,] ResulingMatrix { get; set; }
        }

        public DirectionAndResultingMatrix LCSLength()
        {
            int m = a.Length;
            int n = b.Length;
            directions = new Direction[m + 1, n + 1];
            result = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        result[i, j] = 0;
                    }
                    else if (a[i - 1].Equals(b[j - 1]))
                    {
                        result[i, j] = result[i - 1, j - 1] + 1;
                        directions[i - 1, j - 1] = Direction.TopLeft;
                    }
                    else if (result[i - 1, j] >= result[i, j - 1])
                    {
                        result[i, j] = result[i - 1, j];
                        directions[i, j] = Direction.Top;
                    }
                    else
                    {
                        result[i, j] = result[i, j - 1];
                        directions[i, j] = Direction.Left;
                    }
                }
            }
            return new DirectionAndResultingMatrix() { Directions = directions, ResulingMatrix = result };
        }

        public void PrintLCS(int i, int j)
        {
            if (i == 0 || j == 0)
            {
                return;
            }
            if (directions[i - 1, j - 1] == Direction.TopLeft)
            {
                PrintLCS(i - 1, j - 1);
                Console.WriteLine(a[i - 1]);
            }
            else if (directions[i, j] == Direction.Top)
            {
                PrintLCS(i - 1, j);
            }
            else
            {
                PrintLCS(i, j - 1);
            }
        }
    }
}
