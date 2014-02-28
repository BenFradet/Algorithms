using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public interface IRedBlackNode<T> where T : IComparable<T>
    {
        IRedBlackNode<T> Parent { get; set; }
        IRedBlackNode<T> LeftChild { get; set; }
        IRedBlackNode<T> RightChild { get; set; }
        RedBlackTreeColor Color { get; set; }
        T Key { get; set; }
    }
}
