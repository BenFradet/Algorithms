using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BinarySearchTreeNode
    {
        public BinarySearchTreeNode Parent { get; set; }
        public BinarySearchTreeNode RightChild { get; set; }
        public BinarySearchTreeNode LeftChild { get; set; }
        public int Key { get; set; }

        public BinarySearchTreeNode(int key)
        {
            Key = key;
        }
    }
}
