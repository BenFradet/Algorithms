using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BinarySearchTree
    {
        public BinarySearchTreeNode Root { get; private set; }

        public void InOrderTreeWalk(BinarySearchTreeNode root)
        {
            if (root != null)
            {
                InOrderTreeWalk(root.LeftChild);
                Console.WriteLine(root.Key);
                InOrderTreeWalk(root.RightChild);
            }
        }

        public BinarySearchTreeNode Search(BinarySearchTreeNode root, int key)
        {
            if (root == null || root.Key == key)
            {
                return root;
            }
            if (key < root.Key)
            {
                return Search(root.LeftChild, key);
            }
            else
            {
                return Search(root.RightChild, key);
            }
        }

        public BinarySearchTreeNode IterativeSearch(BinarySearchTreeNode root, int key)
        {
            while (root != null && key != root.Key)
            {
                if (key < root.Key)
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

        public BinarySearchTreeNode Minimum(BinarySearchTreeNode root)
        {
            while (root.LeftChild != null)
            {
                root = root.LeftChild;
            }
            return root;
        }

        public BinarySearchTreeNode Maximum(BinarySearchTreeNode root)
        {
            while (root.RightChild != null)
            {
                root = root.RightChild;
            }
            return root;
        }

        public BinarySearchTreeNode Successor(BinarySearchTreeNode node)
        {
            if (node.RightChild != null)
            {
                return Minimum(node.RightChild);
            }
            BinarySearchTreeNode parentNode = node.Parent;
            while (parentNode != null && node == parentNode.RightChild)
            {
                node = parentNode;
                parentNode = parentNode.Parent;
            }
            return parentNode;
        }

        public BinarySearchTreeNode Predecessor(BinarySearchTreeNode node)
        {
            if (node.LeftChild != null)
            {
                return Maximum(node.LeftChild);
            }
            BinarySearchTreeNode parentNode = node.Parent;
            while (parentNode != null && node == parentNode.LeftChild)
            {
                node = parentNode;
                parentNode = parentNode.Parent;
            }
            return parentNode;
        }

        public void Insert(BinarySearchTreeNode toInsert)
        {
            BinarySearchTreeNode y = null;
            BinarySearchTreeNode x = Root;
            while (x != null)
            {
                y = x;
                if (toInsert.Key < x.Key)
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
            else if (toInsert.Key < y.Key)
            {
                y.LeftChild = toInsert;
            }
            else
            {
                y.RightChild = toInsert;
            }
        }

        public void Delete(BinarySearchTreeNode toDelete)
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
                BinarySearchTreeNode rightMin = Minimum(toDelete.RightChild);
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

        private void Transplant(BinarySearchTreeNode a, BinarySearchTreeNode b)
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
