using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Queue
    {
        private int[] array;
        private int head, tail;

        public Queue(int size)
        {
            array = new int[size];
            head = 0;
            tail = 0;
        }

        public void Enqueue(int toEnqueue)
        {
            if (head == tail + 1 || (head == 0 && tail == array.Length - 1))
                throw new Exception("overflow");
            array[tail] = toEnqueue;
            if (tail == array.Length - 1)
                tail = 0;
            else
                tail++;
        }

        public int Dequeue()
        {
            if (head == tail)
                throw new Exception("underflow");
            else
            {
                int toRet = array[head];
                if (head == array.Length - 1)
                    head = 0;
                else
                    head++;
                return toRet;
            }
        }
    }
}
