using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLTreeNode<T> root;

        public AVLTreeNode<T> Search(T key)
        {
            var node = root;
            while (node != null)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    node = node.LeftChild;
                }
                else if (key.CompareTo(node.Key) > 0)
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

        public void Insert(T key)
        {
            if (root == null)
            {
                root = new AVLTreeNode<T>() { Key = key };
            }
            else
            {
                var node = root;
                while (node != null)
                {
                    if (key.CompareTo(node.Key) <= 0)
                    {
                        var left = node.LeftChild;
                        if (left == null)
                        {
                            node.LeftChild = new AVLTreeNode<T>() { Key = key, Parent = node };
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
                        var right = node.RightChild;
                        if (right == null)
                        {
                            node.RightChild = new AVLTreeNode<T>() { Key = key, Parent = node };
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

        private void InsertBalance(AVLTreeNode<T> node, int balance)
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

                var parent = node.Parent;
                if (parent != null)
                {
                    balance = parent.LeftChild == node ? 1 : -1;
                }
                node = parent;
            }
        }

        public bool Delete(T key)
        {
            var node = root;
            while (node != null)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    node = node.LeftChild;
                }
                else if (key.CompareTo(node.Key) > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    var left = node.LeftChild;
                    var right = node.RightChild;
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
                                var parent = node.Parent;
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
                        var successor = right;
                        if (successor.LeftChild == null)
                        {
                            var parent = node.Parent;
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
                            var parent = node.Parent;
                            var successorParent = successor.Parent;
                            var successorRight = successor.RightChild;
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

        private void DeleteBalance(AVLTreeNode<T> node, int balance)
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

                var parent = node.Parent;
                if (parent != null)
                {
                    balance = parent.LeftChild == node ? -1 : 1;
                }
                node = parent;
            }
        }

        private void Transplant(AVLTreeNode<T> target, AVLTreeNode<T> source)
        {
            var left = source.LeftChild;
            var right = source.RightChild;
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

        private AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
        {
            var right = node.RightChild;
            var rightLeft = right.LeftChild;
            var parent = node.Parent;

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

        private AVLTreeNode<T> RotateLeftRight(AVLTreeNode<T> node)
        {
            var left = node.LeftChild;//=B
            var leftRight = left.RightChild;//=C
            var parent = node.Parent;//=x
            var leftRightRight = leftRight.RightChild;//=d
            var leftRightLeft = leftRight.LeftChild;//=c

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

        private AVLTreeNode<T> RotateRight(AVLTreeNode<T> node)
        {
            var left = node.LeftChild;
            var leftRight = left.RightChild;
            var parent = node.Parent;

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

        private AVLTreeNode<T> RotateRightLeft(AVLTreeNode<T> node)
        {
            var right = node.RightChild;
            var rightLeft = right.LeftChild;
            var parent = node.Parent;
            var rightLeftLeft = rightLeft.LeftChild;
            var rightLeftRight = rightLeft.RightChild;

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
