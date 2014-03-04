using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public abstract class Heap<T> where T : IComparable<T>
    {
        protected T[] array;
        protected int maxSize;
        protected int heapSize;

        protected Heap(int maxSize)
        {
            this.maxSize = maxSize;
            heapSize = 0;
            array = new T[maxSize];
        }

        protected Heap(int maxSize, T[] array)
        {
            if (maxSize < array.Length)
            {
                throw new InvalidOperationException("maxSize should be > to array.length");
            }
            this.maxSize = maxSize;
            heapSize = array.Length;
            this.array = new T[maxSize];
            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = array[i];
            }
            Build();
        }

        public void Insert(T value)
        {
            if (heapSize == maxSize)
            {
                throw new InvalidOperationException("heapSize cannot be > to maxSize");
            }
            heapSize++;
            array[heapSize - 1] = value;
            ChangeKey(heapSize - 1, value);
        }


        public void Build()
        {
            for (int i = heapSize / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public abstract void Heapify(int index);

        public T Extract()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("heap is empty");
            }
            var min = array[0];
            heapSize--;
            array[0] = array[heapSize];
            Heapify(0);
            return min;
        }

        public abstract void ChangeKey(int index, T value);

        //has to be O(heapSize)
        public bool Contains(T value)
        {
            for (int i = 0; i < heapSize; i++)
            {
                if (array[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsEmpty
        {
            get { return heapSize == 0; }
        }
    }
}
