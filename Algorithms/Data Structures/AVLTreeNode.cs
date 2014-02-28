using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class AVLTreeNode<T> where T : IComparable<T>
    {
        public AVLTreeNode<T> Parent { get; set; }
        public AVLTreeNode<T> RightChild { get; set; }
        public AVLTreeNode<T> LeftChild { get; set; }
        public T Key { get; set; }
        public int Balance { get; set; }
    }
}
