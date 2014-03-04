using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class MaxHeap<T> : Heap<T> where T : IComparable<T>
    {
        public MaxHeap(int maxSize) : base(maxSize) { }

        public MaxHeap(int maxSize, T[] array) : base(maxSize, array) { }

        public static T[] Sort(T[] input)
        {
            var heap = new MaxHeap<T>(input.Length, input);
            for (int i = input.Length - 1; i >= 1; i--)
            {
                var tmp = heap.array[0];
                heap.array[0] = heap.array[i];
                heap.heapSize--;
                heap.Heapify(0);
            }
            return heap.array;
        }

        public override void Heapify(int index)
        {
            int left = index * 2 + 1;
            int right = (index + 1) * 2;
            int largest = int.MinValue;
            if (left < heapSize && array[left].CompareTo(array[index]) > 0)
            {
                largest = left;
            }
            else
            {
                largest = index;
            }
            if (right < heapSize && array[right].CompareTo(array[largest]) > 0)
            {
                largest = right;
            }

            if (largest != index)
            {
                var tmp = array[index];
                array[index] = array[largest];
                array[largest] = tmp;
                Heapify(largest);
            }
        }

        public T Maximum()
        {
            return array[0];
        }

        public override void ChangeKey(int index, T value)
        {
            if (array[index].CompareTo(value) > 0)
            {
                throw new InvalidOperationException("the key has to be decreased");
            }
            array[index] = value;
            while (index > 0 && array[(index - 1) / 2].CompareTo(array[index]) < 0)
            {
                var tmp = array[(index - 1) / 2];
                array[(index - 1) / 2] = array[index];
                array[index] = tmp;
                index = (index - 1) / 2;
            }
        }
    }
}
