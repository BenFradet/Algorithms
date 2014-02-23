using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class AVLTreeNode
    {
        public AVLTreeNode Parent { get; set; }
        public AVLTreeNode RightChild { get; set; }
        public AVLTreeNode LeftChild { get; set; }
        public int Key { get; set; }
        public int Balance { get; set; }
    }
}
