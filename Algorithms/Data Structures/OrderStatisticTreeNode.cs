using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class OrderStatisticTreeNode<T> : IRedBlackNode<T> where T : IComparable<T>
    {
        public IRedBlackNode<T> Parent { get; set; }
        public IRedBlackNode<T> LeftChild { get; set; }
        public IRedBlackNode<T> RightChild { get; set; }
        public RedBlackTreeColor Color { get; set; }
        public T Key { get; set; }

        public int Size { get; set; }

        public OrderStatisticTreeNode(T key)
        {
            Key = key;
        }
    }
}
