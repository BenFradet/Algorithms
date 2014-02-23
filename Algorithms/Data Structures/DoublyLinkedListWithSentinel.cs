using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DoublyLinkedListWithSentinel
    {
        DoublyLinkedListNode nil;

        public DoublyLinkedListWithSentinel()
        {
            nil.next = nil;
            nil.prev = nil;
            nil.key = -1;
        }

        public DoublyLinkedListNode Search(int k)
        {
            DoublyLinkedListNode x = nil.next;
            while (x != nil && x.key != k)
                x = x.next;
            return x;
        }

        public void Delete(DoublyLinkedListNode x)
        {
            x.prev.next = x.next;
            x.next.prev = x.prev;
        }

        public void Insert(DoublyLinkedListNode x)
        {
            x.next = nil.next;
            nil.next.prev = x;
            x.prev = nil;
            nil.next = x;
        }
    }
}
