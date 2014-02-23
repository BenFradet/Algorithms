using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class MaxSubarray
    {
        public static List<int> FindMaxSubarray(int[] input, int low, int high)
        {
            if (high == low)
            {
                return new List<int>() { low, high, input[low] };
            }
            else
            {
                int mid = (low + high) / 2;
                //leftLow, leftHigh, leftSum
                List<int> left = FindMaxSubarray(input, low, mid);
                //rightLow, rightHigh, rightSum
                List<int> right = FindMaxSubarray(input, mid + 1, high);
                //crossLow, crossHigh, crossSum
                List<int> cross = FindMaxCrossingSubarray(input, low, mid, high);

                //leftSum >= rightSum && leftSum >= crossSum
                if (left[2] >= right[2] && left[2] >= cross[2])
                {
                    Console.WriteLine("left " + left[2]);
                    return left;
                }
                else if (right[2] >= left[2] && right[2] >= cross[2])
                {
                    Console.WriteLine("right " + right[2]);
                    return right;
                }
                else
                {
                    Console.WriteLine("cross " + cross[2]);
                    return cross;
                }
            }
        }

        private static List<int> FindMaxCrossingSubarray(int[] input, int low, int mid, int high)
        {
            int leftSum = int.MinValue;
            int sum = 0;
            int maxLeft = mid;
            for (int i = mid; i >= low; i--)
            {
                sum += input[i];
                if (sum > leftSum)
                {
                    leftSum = sum;
                    maxLeft = i;
                }
            }

            int rightSum = int.MinValue;
            sum = 0;
            int maxRight = mid + 1;
            for (int i = mid + 1; i <= high; i++)
            {
                sum += input[i];
                if (sum > rightSum)
                {
                    rightSum = sum;
                    maxRight = i;
                }
            }
            return new List<int>() { maxLeft, maxRight, leftSum + rightSum };
        }
    }
}
