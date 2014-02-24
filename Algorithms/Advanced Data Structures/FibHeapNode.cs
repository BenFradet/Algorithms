using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class FibHeapNode<T> where T : IComparable<T>, new()
    {
        public FibHeapNode<T> Child { get; set; }
        public FibHeapNode<T> Parent { get; set; }
        public FibHeapNode<T> LeftSibling { get; set; }
        public FibHeapNode<T> RightSibling { get; set; }
        public bool Mark { get; set; }
        public int Degree { get; set; }
        public T Key { get; set; }
        public List<FibHeapNode<T>> SiblingsList
        {
            get
            {
                var list = new List<FibHeapNode<T>>();
                list.Add(this);
                var next = RightSibling;
                while (next != this)
                {
                    list.Add(next);
                    next = next.RightSibling;
                }
                return list;
            }
        }
    }
}
