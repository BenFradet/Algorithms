using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DoublyLinkedList<T> where T : IComparable<T>
    {
        private DoublyLinkedListNode<T> head;

        public DoublyLinkedList(DoublyLinkedListNode<T> head)
        {
            this.head = head;
        }

        public DoublyLinkedListNode<T> Search(T key)
        {
            var x = head;
            while (x.key.CompareTo(key) != 0 && x != null)
                x = x.next;
            return x;
        }

        public void Insert(DoublyLinkedListNode<T> x)
        {
            x.next = head;
            if (head != null)
                head.prev = x;
            head = x;
            x.prev = null;
        }

        public void Delete(DoublyLinkedListNode<T> x)
        {
            if (x.prev != null)
                x.prev.next = x.next;
            else
                head = x.next;
            if (x.next != null)
                x.next.prev = x.prev;
        }
    }
}
