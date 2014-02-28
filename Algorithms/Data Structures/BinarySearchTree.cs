using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public BinarySearchTreeNode<T> Root { get; private set; }

        public void InOrderTreeWalk(BinarySearchTreeNode<T> root)
        {
            if (root != null)
            {
                InOrderTreeWalk(root.LeftChild);
                Console.WriteLine(root.Key);
                InOrderTreeWalk(root.RightChild);
            }
        }

        public BinarySearchTreeNode<T> Search(BinarySearchTreeNode<T> root, T key)
        {
            if (root == null || root.Key.CompareTo(key) == 0)
            {
                return root;
            }
            if (key.CompareTo(root.Key) < 0)
            {
                return Search(root.LeftChild, key);
            }
            else
            {
                return Search(root.RightChild, key);
            }
        }

        public BinarySearchTreeNode<T> IterativeSearch(BinarySearchTreeNode<T> root, T key)
        {
            while (root != null && key.CompareTo(root.Key) != 0)
            {
                if (key.CompareTo(root.Key) < 0)
                {
                    root = root.LeftChild;
                }
                else
                {
                    root = root.RightChild;
                }
            }
            return root;
        }

        public BinarySearchTreeNode<T> Minimum(BinarySearchTreeNode<T> root)
        {
            while (root.LeftChild != null)
            {
                root = root.LeftChild;
            }
            return root;
        }

        public BinarySearchTreeNode<T> Maximum(BinarySearchTreeNode<T> root)
        {
            while (root.RightChild != null)
            {
                root = root.RightChild;
            }
            return root;
        }

        public BinarySearchTreeNode<T> Successor(BinarySearchTreeNode<T> node)
        {
            if (node.RightChild != null)
            {
                return Minimum(node.RightChild);
            }
            var parentNode = node.Parent;
            while (parentNode != null && node == parentNode.RightChild)
            {
                node = parentNode;
                parentNode = parentNode.Parent;
            }
            return parentNode;
        }

        public BinarySearchTreeNode<T> Predecessor(BinarySearchTreeNode<T> node)
        {
            if (node.LeftChild != null)
            {
                return Maximum(node.LeftChild);
            }
            var parentNode = node.Parent;
            while (parentNode != null && node == parentNode.LeftChild)
            {
                node = parentNode;
                parentNode = parentNode.Parent;
            }
            return parentNode;
        }

        public void Insert(BinarySearchTreeNode<T> toInsert)
        {
            BinarySearchTreeNode<T> y = null;
            var x = Root;
            while (x != null)
            {
                y = x;
                if (toInsert.Key.CompareTo(x.Key) < 0)
                {
                    x = x.LeftChild;
                }
                else
                {
                    x = x.RightChild;
                }
            }
            toInsert.Parent = y;
            if (y == null)
            {
                Root = toInsert;
            }
            else if (toInsert.Key.CompareTo(y.Key) < 0)
            {
                y.LeftChild = toInsert;
            }
            else
            {
                y.RightChild = toInsert;
            }
        }

        public void Delete(BinarySearchTreeNode<T> toDelete)
        {
            if (toDelete.LeftChild == null)
            {
                Transplant(toDelete, toDelete.RightChild);
            }
            else if (toDelete.RightChild == null)
            {
                Transplant(toDelete, toDelete.LeftChild);
            }
            else
            {
                var rightMin = Minimum(toDelete.RightChild);
                if (rightMin.Parent != toDelete)
                {
                    Transplant(rightMin, rightMin.RightChild);
                    rightMin.RightChild = toDelete.RightChild;
                    rightMin.RightChild.Parent = rightMin;
                }
                Transplant(toDelete, rightMin);
                rightMin.LeftChild = toDelete.LeftChild;
                rightMin.LeftChild.Parent = rightMin;
            }
        }

        private void Transplant(BinarySearchTreeNode<T> a, BinarySearchTreeNode<T> b)
        {
            if (a.Parent == null)
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
            if (b != null)
            {
                b.Parent = a.Parent;
            }
        }
    }
}
