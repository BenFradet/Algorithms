﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class OrderStatisticTreeNode : IRedBlackNode
    {
        public IRedBlackNode Parent { get; set; }
        public IRedBlackNode LeftChild { get; set; }
        public IRedBlackNode RightChild { get; set; }
        public RedBlackTreeColor Color { get; set; }
        public int Key { get; set; }

        public int Size { get; set; }

        public OrderStatisticTreeNode(int key)
        {
            Key = key;
        }
    }
}
