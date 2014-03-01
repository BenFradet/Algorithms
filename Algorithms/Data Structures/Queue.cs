using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Queue<T>
    {
        private T[] array;
        private int head, tail;

        public Queue(int size)
        {
            array = new T[size];
            head = 0;
            tail = 0;
        }

        public void Enqueue(T toEnqueue)
        {
            if (head == tail + 1 || (head == 0 && tail == array.Length - 1))
                throw new Exception("overflow");
            array[tail] = toEnqueue;
            if (tail == array.Length - 1)
                tail = 0;
            else
                tail++;
        }

        public T Dequeue()
        {
            if (head == tail)
                throw new Exception("underflow");
            else
            {
                T toRet = array[head];
                if (head == array.Length - 1)
                    head = 0;
                else
                    head++;
                return toRet;
            }
        }

        public bool IsEmpty
        {
            get { return head == tail; }
        }
    }
}
