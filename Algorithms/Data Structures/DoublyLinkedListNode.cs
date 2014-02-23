using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DoublyLinkedListNode
    {
        public DoublyLinkedListNode prev;
        public DoublyLinkedListNode next;
        public int key;

        public DoublyLinkedListNode(int key)
        {
            this.key = key;
        }
    }
}
