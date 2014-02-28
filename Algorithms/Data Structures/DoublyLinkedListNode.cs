using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DoublyLinkedListNode<T> where T : IComparable<T>
    {
        public DoublyLinkedListNode<T> prev;
        public DoublyLinkedListNode<T> next;
        public T key;

        public DoublyLinkedListNode(T key)
        {
            this.key = key;
        }
    }
}
