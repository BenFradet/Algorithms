using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BTreeNode<T>
    {
        public BTreeNode<T>[] Children { get; set; }
        public T[] Keys { get; set; }
        public int KeysCount { get; set; }
        public bool Leaf { get; set; }

        public BTreeNode(int degree)
        {
            Keys = new T[2 * degree - 1];
            Children = new BTreeNode<T>[2 * degree];
        }
    }

    public class BTreeNodeHolder<T>
    {
        public BTreeNode<T> Node { get; set; }
        public int PositionInKeys { get; set; }
    }
}
