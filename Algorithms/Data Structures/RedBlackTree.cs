using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class RedBlackTree<T> where T : IComparable<T>
    {
        private IRedBlackNode<T> nil;
        public IRedBlackNode<T> Root { get; private set; }

        public RedBlackTree(bool orderStatistic = false)
        {
            if (orderStatistic)
            {
                nil = new OrderStatisticTreeNode<T>(default(T));
                (nil as OrderStatisticTreeNode<T>).Size = 0;
            }
            else
            {
                nil = new RedBlackTreeNode<T>(default(T));
            }
            nil.Parent = null;
            nil.LeftChild = null;
            nil.RightChild = null;
            nil.Color = RedBlackTreeColor.Black;
            Root = nil;
        }

        public void InOrderTreeWalk(IRedBlackNode<T> root)
        {
            if (root != nil)
            {
                InOrderTreeWalk(root.LeftChild);
                Console.WriteLine(root.Key + " " + root.Color);
                InOrderTreeWalk(root.RightChild);
            }
        }

        public void Insert(IRedBlackNode<T> node)
        {
            var y = nil;
            var x = Root;
            while (x != nil)
            {
                if (x is OrderStatisticTreeNode<T>)
                {
                    (x as OrderStatisticTreeNode<T>).Size++;
                }

                y = x;
                if (node.Key.CompareTo(x.Key) < 0)
                {
                    x = x.LeftChild;
                }
                else
                {
                    x = x.RightChild;
                }
            }
            node.Parent = y;
            if (y == nil)
            {
                Root = node;
            }
            else if (node.Key.CompareTo(y.Key) < 0)
            {
                y.LeftChild = node;
            }
            else
            {
                y.RightChild = node;
            }
            node.LeftChild = nil;
            node.RightChild = nil;
            node.Color = RedBlackTreeColor.Red;

            if (node is OrderStatisticTreeNode<T>)
            {
                (node as OrderStatisticTreeNode<T>).Size = 1;
            }

            InsertFixup(node);
        }

        protected void InsertFixup(IRedBlackNode<T> node)
        {
            while (node.Parent.Color == RedBlackTreeColor.Red)
            {
                if (node.Parent == node.Parent.Parent.LeftChild)
                {
                    var y = node.Parent.Parent.RightChild;
                    if (y.Color == RedBlackTreeColor.Red)
                    {
                        node.Parent.Color = RedBlackTreeColor.Black;
                        y.Color = RedBlackTreeColor.Black;
                        node.Parent.Parent.Color = RedBlackTreeColor.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.RightChild)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.Color = RedBlackTreeColor.Black;
                        node.Parent.Parent.Color = RedBlackTreeColor.Red;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    var y = node.Parent.Parent.LeftChild;
                    if (y.Color == RedBlackTreeColor.Red)
                    {
                        node.Parent.Color = RedBlackTreeColor.Black;
                        y.Color = RedBlackTreeColor.Black;
                        node.Parent.Parent.Color = RedBlackTreeColor.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.LeftChild)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.Color = RedBlackTreeColor.Black;
                        node.Parent.Parent.Color = RedBlackTreeColor.Red;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            Root.Color = RedBlackTreeColor.Black;
        }

        public void Delete(IRedBlackNode<T> node)
        {
            var y = node;
            RedBlackTreeColor yOriginalColor = y.Color;
            IRedBlackNode<T> x;

            if (y is OrderStatisticTreeNode<T>)
            {
                while (!y.Equals(nil))
                {
                    (y as OrderStatisticTreeNode<T>).Size--;
                    y = y.Parent;
                }
            }

            y = node;
            if (node.LeftChild == nil)
            {
                x = node.RightChild;
                Transplant(node, node.RightChild);
            }
            else if (node.RightChild == nil)
            {
                x = node.LeftChild;
                Transplant(node, node.LeftChild);
            }
            else
            {
                y = Minimum(node.RightChild);//successor
                yOriginalColor = y.Color;
                x = y.RightChild;
                if (y.Parent == node)
                {
                    x.Parent = y;
                }
                else
                {
                    Transplant(y, y.RightChild);
                    y.RightChild = node.RightChild;
                    y.RightChild.Parent = y;
                }
                Transplant(node, y);
                y.LeftChild = node.LeftChild;
                y.LeftChild.Parent = y;
                y.Color = node.Color;
            }
            if (yOriginalColor == RedBlackTreeColor.Black)
            {
                DeleteFixup(x);
            }
        }

        protected void DeleteFixup(IRedBlackNode<T> node)
        {
            while (node != Root && node.Color == RedBlackTreeColor.Black)
            {
                if (node == node.Parent.LeftChild)
                {
                    var w = node.Parent.RightChild;
                    if (w.Color == RedBlackTreeColor.Red)
                    {
                        w.Color = RedBlackTreeColor.Black;
                        node.Parent.Color = RedBlackTreeColor.Red;
                        LeftRotate(node.Parent);
                        w = node.Parent.RightChild;
                    }
                    if (w.LeftChild.Color == RedBlackTreeColor.Black && w.RightChild.Color == RedBlackTreeColor.Black)
                    {
                        w.Color = RedBlackTreeColor.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (w.RightChild.Color == RedBlackTreeColor.Black)
                        {
                            w.LeftChild.Color = RedBlackTreeColor.Black;
                            w.Color = RedBlackTreeColor.Red;
                            RightRotate(w);
                            w = node.Parent.RightChild;
                        }
                        w.Color = node.Parent.Color;
                        node.Parent.Color = RedBlackTreeColor.Black;
                        w.RightChild.Color = RedBlackTreeColor.Black;
                        LeftRotate(node.Parent);
                        node = Root;
                    }
                }
                else
                {
                    var w = node.Parent.LeftChild;
                    if (w.Color == RedBlackTreeColor.Red)
                    {
                        w.Color = RedBlackTreeColor.Black;
                        node.Parent.Color = RedBlackTreeColor.Red;
                        RightRotate(node.Parent);
                        w = node.Parent.LeftChild;
                    }
                    if (w.RightChild.Color == RedBlackTreeColor.Black && w.LeftChild.Color == RedBlackTreeColor.Black)
                    {
                        w.Color = RedBlackTreeColor.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (w.LeftChild.Color == RedBlackTreeColor.Black)
                        {
                            w.RightChild.Color = RedBlackTreeColor.Black;
                            w.Color = RedBlackTreeColor.Red;
                            LeftRotate(w);
                            w = node.Parent.LeftChild;
                        }
                        w.Color = node.Parent.Color;
                        node.Parent.Color = RedBlackTreeColor.Black;
                        w.LeftChild.Color = RedBlackTreeColor.Black;
                        RightRotate(node.Parent);
                        node = Root;
                    }
                }
            }
            node.Color = RedBlackTreeColor.Black;
        }

        protected void Transplant(IRedBlackNode<T> a, IRedBlackNode<T> b)
        {
            if (a.Parent == nil)
            {
                Root = b;
            }
            else if (a == a.Parent.LeftChild)
            {
                a.Parent.LeftChild = b;
            }
            else
            {
                a.Parent.RightChild = b;
            }
            b.Parent = a.Parent;
        }

        protected virtual void LeftRotate(IRedBlackNode<T> node)
        {
            var y = node.RightChild;
            node.RightChild = y.LeftChild;
            if (y.LeftChild != nil)
            {
                y.LeftChild.Parent = node;
            }
            y.Parent = node.Parent;
            if (node.Parent == nil)
            {
                Root = y;
            }
            else if (node == node.Parent.LeftChild)
            {
                node.Parent.LeftChild = y;
            }
            else
            {
                node.Parent.RightChild = y;
            }
            y.LeftChild = node;
            node.Parent = y;

            if (y is OrderStatisticTreeNode<T> && node is OrderStatisticTreeNode<T>
                && node.LeftChild is OrderStatisticTreeNode<T> && node.RightChild is OrderStatisticTreeNode<T>)
            {
                (y as OrderStatisticTreeNode<T>).Size = (node as OrderStatisticTreeNode<T>).Size;
                (node as OrderStatisticTreeNode<T>).Size = (node.RightChild as OrderStatisticTreeNode<T>).Size
                    + (node.LeftChild as OrderStatisticTreeNode<T>).Size + 1;
            }
        }

        protected virtual void RightRotate(IRedBlackNode<T> node)
        {
            var y = node.LeftChild;
            node.LeftChild = y.RightChild;
            if (y.RightChild != nil)
            {
                y.RightChild.Parent = node;
            }
            y.Parent = node.Parent;
            if (node.Parent == nil)
            {
                Root = y;
            }
            else if (node == node.Parent.RightChild)
            {
                node.Parent.RightChild = y;
            }
            else
            {
                node.Parent.LeftChild = y;
            }
            y.RightChild = node;
            node.Parent = y;

            if (y is OrderStatisticTreeNode<T> && node is OrderStatisticTreeNode<T>
                && node.LeftChild is OrderStatisticTreeNode<T> && node.RightChild is OrderStatisticTreeNode<T>)
            {
                (y as OrderStatisticTreeNode<T>).Size = (node as OrderStatisticTreeNode<T>).Size;
                (node as OrderStatisticTreeNode<T>).Size = (node.RightChild as OrderStatisticTreeNode<T>).Size
                    + (node.LeftChild as OrderStatisticTreeNode<T>).Size + 1;
            }
        }

        public IRedBlackNode<T> Minimum(IRedBlackNode<T> root)
        {
            while (root.LeftChild != nil)
            {
                root = root.LeftChild;
            }
            return root;
        }

        public IRedBlackNode<T> Maximum(IRedBlackNode<T> root)
        {
            while (root.RightChild != nil)
            {
                root = root.RightChild;
            }
            return root;
        }

        public OrderStatisticTreeNode<T> Select(IRedBlackNode<T> root, int ithSmallest)
        {
            if (root is OrderStatisticTreeNode<T> && root.LeftChild is OrderStatisticTreeNode<T>)
            {
                int rank = (root.LeftChild as OrderStatisticTreeNode<T>).Size + 1;
                if (ithSmallest == rank)
                {
                    return (root as OrderStatisticTreeNode<T>);
                }
                else if (ithSmallest < rank)
                {
                    return Select(root.LeftChild, ithSmallest);
                }
                else
                {
                    return Select(root.RightChild, ithSmallest - rank);
                }
            }
            else
            {
                return null;
            }
        }

        public int Rank(IRedBlackNode<T> root)
        {
            if (root is OrderStatisticTreeNode<T> && root.LeftChild is OrderStatisticTreeNode<T>)
            {
                int rank = (root.LeftChild as OrderStatisticTreeNode<T>).Size + 1;
                var y = root;
                while (y != root)
                {
                    if (y == y.Parent.RightChild)
                    {
                        rank += (y.Parent.LeftChild as OrderStatisticTreeNode<T>).Size + 1;
                    }
                    y = y.Parent;
                }
                return rank;
            }
            else
            {
                return -1;
            }
        }
    }
}
