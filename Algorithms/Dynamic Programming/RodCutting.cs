using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class RodCutting
    {
        private int[] prices;

        public RodCutting()
        {
            prices = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };
        }

        public int CutRod(int size)
        {
            if (size == 0)
            {
                return 0;
            }
            int maxRevenue = int.MinValue;
            for (int i = 1; i <= size; i++)
            {
                maxRevenue = Math.Max(maxRevenue, prices[i - 1] + CutRod(size - i));
            }
            return maxRevenue;
        }

        public int BottomUpCutRod(int size)
        {
            var revenues = new int[size + 1];
            revenues[0] = 0;
            for (int i = 1; i < size + 1; i++)
            {
                int maxRevenue = int.MinValue;
                for (int j = 1; j <= i; j++)
                {
                    maxRevenue = Math.Max(maxRevenue, prices[j - 1] + revenues[i - j]);
                }
                revenues[i] = maxRevenue;
            }
            return revenues[size];
        }

        private struct RevenuesAndSize
        {
            public int[] Revenues { get; set; }
            public int[] Sizes { get; set; }
        }

        private RevenuesAndSize BottomUpCutRodWithSolution(int size)
        {
            RevenuesAndSize result = new RevenuesAndSize() { Revenues = new int[size + 1], Sizes = new int[size + 1] };
            result.Revenues[0] = 0;
            for (int i = 1; i < size + 1; i++)
            {
                int maxRevenue = int.MinValue;
                for (int j = 1; j <= i; j++)
                {
                    if (maxRevenue < prices[j - 1] + result.Revenues[i - j])
                    {
                        maxRevenue = prices[j - 1] + result.Revenues[i - j];
                        result.Sizes[i] = j;
                    }
                }
                result.Revenues[i] = maxRevenue;
            }
            return result;
        }

        public void PrintBottomUpCutRodWithSolution(int size)
        {
            RevenuesAndSize result = BottomUpCutRodWithSolution(size);
            while (size > 0)
            {
                Console.WriteLine(result.Sizes[size - 1]);
                size -= result.Sizes[size - 1];
            }
        }

        public int TopDownMemoizedCutRod(int size)
        {
            var revenues = new int[size + 1];
            for (int i = 0; i < size + 1; i++)
            {
                revenues[i] = int.MinValue;
            }
            return TopDownMemoizedCutRodAux(size, revenues);
        }

        private int TopDownMemoizedCutRodAux(int size, int[] revenues)
        {
            if (revenues[size] >= 0)
            {
                return revenues[size];
            }
            int maxRevenue;
            if (size == 0)
            {
                maxRevenue = 0;
            }
            else
            {
                maxRevenue = int.MinValue;
                for (int i = 1; i <= size; i++)
                {
                    maxRevenue = Math.Max(maxRevenue, prices[i - 1] + TopDownMemoizedCutRodAux(size - i, revenues));
                }
            }
            revenues[size] = maxRevenue;
            return maxRevenue;
        }
    }
}
