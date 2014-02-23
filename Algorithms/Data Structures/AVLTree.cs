using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class AVLTree
    {
        private AVLTreeNode root;

        public AVLTreeNode Search(int key)
        {
            AVLTreeNode node = root;
            while (node != null)
            {
                if (key < node.Key)
                {
                    node = node.LeftChild;
                }
                else if (key > node.Key)
                {
                    node = node.RightChild;
                }
                else
                {
                    return node;
                }
            }
            return null;
        }

        public void Insert(int key)
        {
            if (root == null)
            {
                root = new AVLTreeNode() { Key = key };
            }
            else
            {
                AVLTreeNode node = root;
                while (node != null)
                {
                    if (key <= node.Key)
                    {
                        AVLTreeNode left = node.LeftChild;
                        if (left == null)
                        {
                            node.LeftChild = new AVLTreeNode() { Key = key, Parent = node };
                            InsertBalance(node, 1);
                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else
                    {
                        AVLTreeNode right = node.RightChild;
                        if (right == null)
                        {
                            node.RightChild = new AVLTreeNode() { Key = key, Parent = node };
                            InsertBalance(node, -1);
                            return;
                        }
                        else
                        {
                            node = right;
                        }
                    }
                }
            }
        }

        private void InsertBalance(AVLTreeNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);
                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.LeftChild.Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }
                    return;
                }
                else if (balance == -2)
                {
                    if (node.RightChild.Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }
                    return;
                }

                AVLTreeNode parent = node.Parent;
                if (parent != null)
                {
                    balance = parent.LeftChild == node ? 1 : -1;
                }
                node = parent;
            }
        }

        public bool Delete(int key)
        {
            AVLTreeNode node = root;
            while (node != null)
            {
                if (key < node.Key)
                {
                    node = node.LeftChild;
                }
                else if (key > node.Key)
                {
                    node = node.RightChild;
                }
                else
                {
                    AVLTreeNode left = node.LeftChild;
                    AVLTreeNode right = node.RightChild;
                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == root)
                            {
                                root = null;
                            }
                            else
                            {
                                AVLTreeNode parent = node.Parent;
                                if (parent.LeftChild == node)
                                {
                                    parent.LeftChild = null;
                                    DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.RightChild = null;
                                    DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Transplant(node, right);
                            DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Transplant(node, left);
                        DeleteBalance(node, 0);
                    }
                    else
                    {
                        AVLTreeNode successor = right;
                        if (successor.LeftChild == null)
                        {
                            AVLTreeNode parent = node.Parent;
                            successor.Parent = parent;
                            successor.LeftChild = left;
                            successor.Balance = node.Balance;
                            if (left != null)
                            {
                                left.Parent = successor;
                            }
                            if (node == root)
                            {
                                root = successor;
                            }
                            else
                            {
                                if (parent.LeftChild == node)
                                {
                                    parent.LeftChild = successor;
                                }
                                else
                                {
                                    parent.RightChild = successor;
                                }
                            }
                            DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.LeftChild != null)
                            {
                                successor = successor.LeftChild;
                            }
                            AVLTreeNode parent = node.Parent;
                            AVLTreeNode successorParent = successor.Parent;
                            AVLTreeNode successorRight = successor.RightChild;
                            if (successorParent.LeftChild == successor)
                            {
                                successorParent.LeftChild = successorRight;
                            }
                            else
                            {
                                successorParent.RightChild = successorRight;
                            }
                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }
                            successor.Parent = parent;
                            successor.LeftChild = left;
                            successor.Balance = node.Balance;
                            successor.RightChild = right;
                            right.Parent = successor;
                            if (left != null)
                            {
                                left.Parent = successor;
                            }
                            if (node == root)
                            {
                                root = successor;
                            }
                            else
                            {
                                if (parent.LeftChild == node)
                                {
                                    parent.LeftChild = successor;
                                }
                                else
                                {
                                    parent.RightChild = successor;
                                }
                            }
                            DeleteBalance(successorParent, -1);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private void DeleteBalance(AVLTreeNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);
                if (balance == 2)
                {
                    if (node.LeftChild.Balance >= 0)
                    {
                        node = RotateRight(node);
                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.RightChild.Balance <= 0)
                    {
                        node = RotateLeftRight(node);
                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                AVLTreeNode parent = node.Parent;
                if (parent != null)
                {
                    balance = parent.LeftChild == node ? -1 : 1;
                }
                node = parent;
            }
        }

        private void Transplant(AVLTreeNode target, AVLTreeNode source)
        {
            AVLTreeNode left = source.LeftChild;
            AVLTreeNode right = source.RightChild;
            target.Balance = source.Balance;
            target.Key = source.Key;
            target.LeftChild = left;
            target.RightChild = right;
            if (left != null)
            {
                left.Parent = target;
            }
            if (right != null)
            {
                right.Parent = target;
            }
        }

        private AVLTreeNode RotateLeft(AVLTreeNode node)
        {
            AVLTreeNode right = node.RightChild;
            AVLTreeNode rightLeft = right.LeftChild;
            AVLTreeNode parent = node.Parent;

            right.Parent = parent;
            right.LeftChild = node;
            node.RightChild = rightLeft;
            node.Parent = right;
            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }
            if (node == root)
            {
                root = right;
            }
            else if (parent.RightChild == node)
            {
                parent.RightChild = right;
            }
            else
            {
                parent.LeftChild = right;
            }

            right.Balance++;
            node.Balance = -right.Balance;
            return right;
        }

        private AVLTreeNode RotateLeftRight(AVLTreeNode node)
        {
            AVLTreeNode left = node.LeftChild;//=B
            AVLTreeNode leftRight = left.RightChild;//=C
            AVLTreeNode parent = node.Parent;//=x
            AVLTreeNode leftRightRight = leftRight.RightChild;//=d
            AVLTreeNode leftRightLeft = leftRight.LeftChild;//=c

            leftRight.Parent = parent;
            node.LeftChild = leftRightRight;
            left.RightChild = leftRightLeft;
            leftRight.LeftChild = left;
            leftRight.RightChild = node;
            left.Parent = leftRight;
            node.Parent = leftRight;
            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }
            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }
            if (node == root)
            {
                root = leftRight;
            }
            else if (parent.LeftChild == node)
            {
                parent.LeftChild = leftRight;
            }
            else
            {
                parent.RightChild = leftRight;
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }
            leftRight.Balance = 0;
            return leftRight;
        }

        private AVLTreeNode RotateRight(AVLTreeNode node)
        {
            AVLTreeNode left = node.LeftChild;
            AVLTreeNode leftRight = left.RightChild;
            AVLTreeNode parent = node.Parent;

            left.Parent = parent;
            left.RightChild = node;
            node.LeftChild = leftRight;
            node.Parent = left;
            if (leftRight != null)
            {
                leftRight.Parent = node;
            }
            if (node == root)
            {
                root = left;
            }
            else if (parent.RightChild == node)
            {
                parent.RightChild = left;
            }
            else
            {
                parent.LeftChild = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;
            return left;
        }

        private AVLTreeNode RotateRightLeft(AVLTreeNode node)
        {
            AVLTreeNode right = node.RightChild;
            AVLTreeNode rightLeft = right.LeftChild;
            AVLTreeNode parent = node.Parent;
            AVLTreeNode rightLeftLeft = rightLeft.LeftChild;
            AVLTreeNode rightLeftRight = rightLeft.RightChild;

            rightLeft.Parent = parent;
            rightLeft.LeftChild = node;
            rightLeft.RightChild = right;
            node.RightChild = rightLeftLeft;
            right.LeftChild = rightLeftRight;
            node.Parent = rightLeft;
            right.Parent = rightLeft;
            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }
            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }
            if (node == root)
            {
                root = rightLeftRight;
            }
            else if (parent.LeftChild == node)
            {
                parent.LeftChild = rightLeft;
            }
            else
            {
                parent.RightChild = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }
            rightLeft.Balance = 0;
            return rightLeft;
        }
    }
}
