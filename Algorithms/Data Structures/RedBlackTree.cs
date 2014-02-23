using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class RedBlackTree
    {
        private IRedBlackNode nil;
        public IRedBlackNode Root { get; private set; }

        public RedBlackTree(bool orderStatistic = false)
        {
            if (orderStatistic)
            {
                nil = new OrderStatisticTreeNode(-1);
                (nil as OrderStatisticTreeNode).Size = 0;
            }
            else
            {
                nil = new RedBlackTreeNode(-1);
            }
            nil.Parent = null;
            nil.LeftChild = null;
            nil.RightChild = null;
            nil.Color = RedBlackTreeColor.Black;
            Root = nil;
        }

        public void InOrderTreeWalk(IRedBlackNode root)
        {
            if (root != nil)
            {
                InOrderTreeWalk(root.LeftChild);
                Console.WriteLine(root.Key + " " + root.Color);
                InOrderTreeWalk(root.RightChild);
            }
        }

        public void Insert(IRedBlackNode node)
        {
            IRedBlackNode y = nil;
            IRedBlackNode x = Root;
            while (x != nil)
            {
                if (x is OrderStatisticTreeNode)
                {
                    (x as OrderStatisticTreeNode).Size++;
                }

                y = x;
                if (node.Key < x.Key)
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
            else if (node.Key < y.Key)
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

            if (node is OrderStatisticTreeNode)
            {
                (node as OrderStatisticTreeNode).Size = 1;
            }

            InsertFixup(node);
        }

        protected void InsertFixup(IRedBlackNode node)
        {
            while (node.Parent.Color == RedBlackTreeColor.Red)
            {
                if (node.Parent == node.Parent.Parent.LeftChild)
                {
                    IRedBlackNode y = node.Parent.Parent.RightChild;
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
                    IRedBlackNode y = node.Parent.Parent.LeftChild;
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

        public void Delete(IRedBlackNode node)
        {
            IRedBlackNode y = node;
            RedBlackTreeColor yOriginalColor = y.Color;
            IRedBlackNode x;

            if (y is OrderStatisticTreeNode)
            {
                while (!y.Equals(nil))
                {
                    (y as OrderStatisticTreeNode).Size--;
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

        protected void DeleteFixup(IRedBlackNode node)
        {
            while (node != Root && node.Color == RedBlackTreeColor.Black)
            {
                if (node == node.Parent.LeftChild)
                {
                    IRedBlackNode w = node.Parent.RightChild;
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
                    IRedBlackNode w = node.Parent.LeftChild;
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

        protected void Transplant(IRedBlackNode a, IRedBlackNode b)
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

        protected virtual void LeftRotate(IRedBlackNode node)
        {
            IRedBlackNode y = node.RightChild;
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

            if (y is OrderStatisticTreeNode && node is OrderStatisticTreeNode
                && node.LeftChild is OrderStatisticTreeNode && node.RightChild is OrderStatisticTreeNode)
            {
                (y as OrderStatisticTreeNode).Size = (node as OrderStatisticTreeNode).Size;
                (node as OrderStatisticTreeNode).Size = (node.RightChild as OrderStatisticTreeNode).Size
                    + (node.LeftChild as OrderStatisticTreeNode).Size + 1;
            }
        }

        protected virtual void RightRotate(IRedBlackNode node)
        {
            IRedBlackNode y = node.LeftChild;
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

            if (y is OrderStatisticTreeNode && node is OrderStatisticTreeNode
                && node.LeftChild is OrderStatisticTreeNode && node.RightChild is OrderStatisticTreeNode)
            {
                (y as OrderStatisticTreeNode).Size = (node as OrderStatisticTreeNode).Size;
                (node as OrderStatisticTreeNode).Size = (node.RightChild as OrderStatisticTreeNode).Size
                    + (node.LeftChild as OrderStatisticTreeNode).Size + 1;
            }
        }

        public IRedBlackNode Minimum(IRedBlackNode root)
        {
            while (root.LeftChild != nil)
            {
                root = root.LeftChild;
            }
            return root;
        }

        public IRedBlackNode Maximum(IRedBlackNode root)
        {
            while (root.RightChild != nil)
            {
                root = root.RightChild;
            }
            return root;
        }

        public OrderStatisticTreeNode Select(IRedBlackNode root, int ithSmallest)
        {
            if (root is OrderStatisticTreeNode && root.LeftChild is OrderStatisticTreeNode)
            {
                int rank = (root.LeftChild as OrderStatisticTreeNode).Size + 1;
                if (ithSmallest == rank)
                {
                    return (root as OrderStatisticTreeNode);
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

        public int Rank(IRedBlackNode root)
        {
            if (root is OrderStatisticTreeNode && root.LeftChild is OrderStatisticTreeNode)
            {
                int rank = (root.LeftChild as OrderStatisticTreeNode).Size + 1;
                IRedBlackNode y = root;
                while (y != root)
                {
                    if (y == y.Parent.RightChild)
                    {
                        rank += (y.Parent.LeftChild as OrderStatisticTreeNode).Size + 1;
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
