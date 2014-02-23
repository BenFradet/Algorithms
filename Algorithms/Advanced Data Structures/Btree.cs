using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BTree<T> where T : IComparable<T>
    {
        public BTreeNode<T> Root { get; private set; }
        public int Degree { get; private set; }

        BTree(int degree)
        {
            var x = new BTreeNode<T>(degree) { Leaf = true, KeysCount = 0 };
            Degree = degree;
            Root = x;
        }

        public BTreeNodeHolder<T> Search(T key)
        {
            return Search(Root, key);
        }

        private BTreeNodeHolder<T> Search(BTreeNode<T> node, T key)
        {
            int i = 0;
            while (i < node.KeysCount && key.CompareTo(node.Keys[i]) > 0)
            {
                i++;
            }
            if (i < node.KeysCount && key.CompareTo(node.Keys[i]) == 0)
            {
                return new BTreeNodeHolder<T>() { Node = node, PositionInKeys = i };
            }
            else if (node.Leaf)
            {
                return null;
            }
            else
            {
                return Search(node.Children[i], key);
            }
        }

        public void Insert(T key)
        {
            var r = Root;
            if (r.KeysCount == 2 * Degree - 1)
            {
                var s = new BTreeNode<T>(Degree) { Leaf = false, KeysCount = 0 };
                s.Children[0] = r;
                Root = s;
                SplitChild(s, 0);
                InsertNonFull(s, key);
            }
            else
            {
                InsertNonFull(r, key);
            }
        }

        private void SplitChild(BTreeNode<T> node, int i)
        {
            var y = node.Children[i];
            var z = new BTreeNode<T>(Degree) { Leaf = y.Leaf, KeysCount = Degree - 1 };
            for (int j = 0; j < Degree - 1; j++)
            {
                z.Keys[j] = y.Keys[j + Degree];
            }
            if (!y.Leaf)
            {
                for (int j = 0; j < Degree; j++)
                {
                    z.Children[j] = y.Children[j + Degree];
                }
            }
            y.KeysCount = Degree - 1;
            for (int j = node.KeysCount + 1; j >= i + 1; j--)
            {
                node.Children[j + 1] = node.Children[j];
            }
            node.Children[i + 1] = z;
            for (int j = node.KeysCount - 1; j >= i; j--)
            {
                node.Keys[j + 1] = node.Keys[j];
            }
            node.Keys[i] = y.Keys[Degree];
            node.KeysCount++;
        }

        private void InsertNonFull(BTreeNode<T> node, T key)
        {
            var i = node.KeysCount;
            if (node.Leaf)
            {
                while (i >= 0 && key.CompareTo(node.Keys[i]) < 0)
                {
                    node.Keys[i + 1] = node.Keys[i];
                    i--;
                }
                node.Keys[i + 1] = key;
                node.KeysCount++;
            }
            else
            {
                while (i >= 0 && key.CompareTo(node.Keys[i]) < 0)
                {
                    i--;
                }
                i++;
                if (node.Children[i].KeysCount == 2 * Degree - 1)
                {
                    SplitChild(node, i);
                    if (key.CompareTo(node.Keys[i]) > 0)
                    {
                        i++;
                    }
                }
                InsertNonFull(node.Children[i], key);
            }
        }
    }
}
