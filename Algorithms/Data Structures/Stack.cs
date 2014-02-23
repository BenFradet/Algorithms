using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Stack
    {
        private int[] array;
        private int top;

        public Stack(int size)
        {
            array = new int[size];
            top = -1;
        }

        bool IsEmpty()
        {
            return top == 0;
        }

        void Push(int toPush)
        {
            if (top == array.Length - 1)
                throw new Exception("overflow");
            else
            {
                top++;
                array[top] = toPush;
            }
        }

        int Pop()
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
