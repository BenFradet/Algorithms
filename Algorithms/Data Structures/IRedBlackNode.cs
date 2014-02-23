using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public interface IRedBlackNode
    {
        IRedBlackNode Parent { get; set; }
        IRedBlackNode LeftChild { get; set; }
        IRedBlackNode RightChild { get; set; }
        RedBlackTreeColor Color { get; set; }
        int Key { get; set; }
    }
}
