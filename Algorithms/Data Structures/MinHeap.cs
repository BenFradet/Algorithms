using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class MinHeap<T> : Heap<T> where T : IComparable<T>
    {
        public MinHeap(int maxSize) : base(maxSize) { }

        public MinHeap(int maxSize, T[] array) : base(maxSize, array) { }

        public static List<T> Sort(T[] input)
        {
            var list = new List<T>();
            var heap = new MinHeap<T>(input.Length, input);
            for (int i = input.Length - 1; i >= 0; i--)
            {
                list.Add(heap.array[0]);
                var tmp = heap.array[0];
                heap.array[0] = heap.array[i];
                heap.heapSize--;
                heap.Heapify(0);
            }
            return list;
        }

        public override void Heapify(int index)
        {
            int left = index * 2 + 1;
            int right = (index + 1) * 2;
            int smallest = int.MaxValue;
            if (left < heapSize && array[left].CompareTo(array[index]) < 0)
            {
                smallest = left;
            }
            else
            {
                smallest = index;
            }
            if (right < heapSize && array[right].CompareTo(array[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                var tmp = array[index];
                array[index] = array[smallest];
                array[smallest] = tmp;
                Heapify(smallest);
            }
        }

        public T Minimum()
        {
            return array[0];
        }

        public override void ChangeKey(int index, T value)
        {
            if (array[index].CompareTo(value) < 0)
            {
                throw new InvalidOperationException("the key has to be decreased");
            }
            array[index] = value;
            while (index > 0 && array[(index - 1) / 2].CompareTo(array[index]) > 0)
            {
                var tmp = array[(index - 1) / 2];
                array[(index - 1) / 2] = array[index];
                array[index] = tmp;
                index = (index - 1) / 2;
            }
        }
    }
}
