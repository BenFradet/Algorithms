using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class HeapSortClass
    {
        static int heapSize = int.MinValue;

        public static void HeapSort(int[] input)
        {
            BuildMaxHeap(input);
            for (int i = input.Length - 1; i >= 1; i--)
            {
                int tmp = input[0];
                input[0] = input[i];
                input[i] = tmp;
                heapSize--;
                MaxHeapify(input, 0);
            }
        }

        public static void BuildMaxHeap(int[] input)
        {
            heapSize = input.Length;
            for (int i = input.Length / 2; i >= 0; i--)
            {
                MaxHeapify(input, i);
            }
        }

        public static void MaxHeapify(int[] input, int index)
        {
            int left = index * 2 + 1;
            int right = (index + 1) * 2;
            int largest = int.MinValue;

            if (left < heapSize && input[left] > input[index])
            {
                largest = left;
            }
            else
            {
                largest = index;
            }

            if (right < heapSize && input[right] > input[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                int tmp = input[index];
                input[index] = input[largest];
                input[largest] = tmp;
                MaxHeapify(input, largest);
            }
        }

        public static int HeapMaximum(int[] input)
        {
            return input[0];
        }

        public static int HeapExtractMax(int[] input)
        {
            int max = input[0];
            input[0] = input[heapSize];
            heapSize--;
            MaxHeapify(input, 0);
            return max;
        }

        public static void HeapIncreaseKey(int[] input, int index, int value)
        {
            input[index] = value;
            while (index > 0 && input[(index - 1) / 2] < input[index])
            {
                int tmp = input[(index - 1) / 2];
                input[(index - 1) / 2] = input[index];
                input[index] = tmp;
                index = (index - 1) / 2;
            }
        }

        public static void HeapInsert(int[] input, int value)
        {
            heapSize++;
            input[heapSize] = int.MinValue;
            HeapIncreaseKey(input, heapSize - 1, value);
        }

        public static void BuildMinHeap(int[] input)
        {
            heapSize = input.Length;
            for (int i = input.Length / 2; i >= 0; i--)
            {
                MinHeapify(input, i);
            }
        }

        public static void MinHeapify(int[] input, int index)
        {
            int left = index * 2 + 1;
            int right = (index + 1) * 2;
            int smallest = int.MaxValue;

            if (left < heapSize && input[left] < input[index])
            {
                smallest = left;
            }
            else
            {
                smallest = index;
            }

            if (right < heapSize && input[right] < input[smallest])
            {
                smallest = right;
            }

            if (smallest != index)
            {
                int tmp = input[index];
                input[index] = input[smallest];
                input[smallest] = tmp;
                MinHeapify(input, smallest);
            }
        }
    }
}
