using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DoublyLinkedListWithSentinel<T> where T : IComparable<T>
    {
        DoublyLinkedListNode<T> nil;

        public DoublyLinkedListWithSentinel()
        {
            nil.next = nil;
            nil.prev = nil;
            nil.key = default(T);
        }

        public DoublyLinkedListNode<T> Search(T key)
        {
            var x = nil.next;
            while (x != nil && x.key.CompareTo(key) != 0)
                x = x.next;
            return x;
        }

        public void Delete(DoublyLinkedListNode<T> x)
        {
            x.prev.next = x.next;
            x.next.prev = x.prev;
        }

        public void Insert(DoublyLinkedListNode<T> x)
        {
            x.next = nil.next;
            nil.next.prev = x;
            x.prev = nil;
            nil.next = x;
        }
    }
}
