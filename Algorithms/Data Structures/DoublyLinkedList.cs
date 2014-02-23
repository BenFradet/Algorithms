using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DoublyLinkedList
    {
        private DoublyLinkedListNode head;

        public DoublyLinkedList(DoublyLinkedListNode head)
        {
            this.head = head;
        }

        public DoublyLinkedListNode Search(int k)
        {
            DoublyLinkedListNode x = head;
            while (x.key != k && x != null)
                x = x.next;
            return x;
        }

        public void Insert(DoublyLinkedListNode x)
        {
            x.next = head;
            if (head != null)
                head.prev = x;
            head = x;
            x.prev = null;
        }

        public void Delete(DoublyLinkedListNode x)
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
