using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BinarySearchTreeNode<T> where T : IComparable<T>
    {
        public BinarySearchTreeNode<T> Parent { get; set; }
        public BinarySearchTreeNode<T> RightChild { get; set; }
        public BinarySearchTreeNode<T> LeftChild { get; set; }
        public T Key { get; set; }

        public BinarySearchTreeNode(T key)
        {
            Key = key;
        }
    }
}
