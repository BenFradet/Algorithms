using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class HeapSortClass<T> where T : IComparable<T>
    {
        private static int heapSize = int.MinValue;

        public static void HeapSort(T[] input)
        {
            BuildMaxHeap(input);
            for (int i = input.Length - 1; i >= 1; i--)
            {
                var tmp = input[0];
                input[0] = input[i];
                input[i] = tmp;
                heapSize--;
                MaxHeapify(input, 0);
            }
        }

        public static void BuildMaxHeap(T[] input)
        {
            heapSize = input.Length;
            for (int i = input.Length / 2; i >= 0; i--)
            {
                MaxHeapify(input, i);
            }
        }

        public static void MaxHeapify(T[] input, int index)
        {
            int left = index * 2 + 1;
            int right = (index + 1) * 2;
            int largest = int.MinValue;

            if (left < heapSize && input[left].CompareTo(input[index]) > 0)
            {
                largest = left;
            }
            else
            {
                largest = index;
            }

            if (right < heapSize && input[right].CompareTo(input[largest]) > 0)
            {
                largest = right;
            }

            if (largest != index)
            {
                var tmp = input[index];
                input[index] = input[largest];
                input[largest] = tmp;
                MaxHeapify(input, largest);
            }
        }

        public static T HeapMaximum(T[] input)
        {
            return input[0];
        }

        public static T HeapExtractMax(T[] input)
        {
            var max = input[0];
            heapSize--;
            input[0] = input[heapSize];
            MaxHeapify(input, 0);
            return max;
        }

        public static void HeapIncreaseKey(T[] input, int index, T value)
        {
            input[index] = value;
            while (index > 0 && input[(index - 1) / 2].CompareTo(input[index]) < 0)
            {
                var tmp = input[(index - 1) / 2];
                input[(index - 1) / 2] = input[index];
                input[index] = tmp;
                index = (index - 1) / 2;
            }
        }

        //might not work with generics
        public static void HeapInsert(T[] input, T value)
        {
            heapSize++;
            //need min value for T
            input[heapSize] = default(T);
            HeapIncreaseKey(input, heapSize - 1, value);
        }

        public static void HeapSortMin(T[] input)
        {
            BuildMinHeap(input);
            for (int i = input.Length - 1; i >= 1; i--)
            {
                var tmp = input[0];
                input[0] = input[i];
                input[i] = tmp;
                heapSize--;
                MinHeapify(input, 0);
            }
        }

        public static void BuildMinHeap(T[] input)
        {
            heapSize = input.Length;
            for (int i = input.Length / 2; i >= 0; i--)
            {
                MinHeapify(input, i);
            }
        }

        public static void MinHeapify(T[] input, int index)
        {
            int left = index * 2 + 1;
            int right = (index + 1) * 2;
            int smallest = int.MaxValue;

            if (left < heapSize && input[left].CompareTo(input[index]) < 0)
            {
                smallest = left;
            }
            else
            {
                smallest = index;
            }

            if (right < heapSize && input[right].CompareTo(input[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                var tmp = input[index];
                input[index] = input[smallest];
                input[smallest] = tmp;
                MinHeapify(input, smallest);
            }
        }

        public static T HeapMinimum(T[] input)
        {
            return input[0];
        }

        public static T HeapExtractMin(T[] input)
        {
            var min = input[0];
            heapSize--;
            input[0] = input[heapSize];
            MinHeapify(input, 0);
            return min;
        }

        public static void HeapDecreaseKey(T[] input, int index, T value)
        {
            input[index] = value;
            while (index > 0 && input[(index - 1) / 2].CompareTo(input[index]) > 0)
            {
                var tmp = input[(index - 1) / 2];
                input[(index - 1) / 2] = input[index];
                input[index] = tmp;
                index = (index - 1) / 2;
            }
        }

        //to refactor
        public static bool IsEmpty
        {
            get { return heapSize == 0; }
        }

        public static bool Contains(T[] input, T value)
        {
            for (int i = 0; i < heapSize; i++)
            {
                if (input[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
