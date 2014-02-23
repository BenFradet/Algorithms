using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BTreeNode<T>
    {
        public BTreeNode<T> Right { get; set; }
        public BTreeNode<T> Left { get; set; }
        public T[] keys { get; set; }
        public bool Leaf { get; set; }
    }
}
