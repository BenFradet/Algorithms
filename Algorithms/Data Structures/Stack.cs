using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Stack<T>
    {
        private T[] array;
        private int top;

        public Stack(int size)
        {
            array = new T[size];
            top = -1;
        }

        public bool IsEmpty()
        {
            return top == 0;
        }

        public void Push(T toPush)
        {
            if (top == array.Length - 1)
                throw new Exception("overflow");
            else
            {
                top++;
                array[top] = toPush;
            }
        }

        public T Pop()
        {
            if (top == -1)
                throw new Exception("underflow");
            else
            {
                top--;
                return array[top + 1];
            }
        }
    }
}
