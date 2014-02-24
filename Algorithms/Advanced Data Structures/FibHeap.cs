using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class FibHeap<T> where T : IComparable<T>, new()
    {
        private double phi = (1 + Math.Sqrt(5)) / 2;
        private FibHeapNode<T> min;
        private int nodeCount;

        public FibHeap()
        {
            min = null;
            nodeCount = 0;
        }

        public FibHeapNode<T> Minimum
        {
            get { return min; }
        }

        //node.Key already filled in
        public void Insert(FibHeapNode<T> node)
        {
            node.Degree = 0;
            node.Parent = null;
            node.Child = null;
            node.LeftSibling = node;
            node.RightSibling = node;
            node.Mark = false;

            min = ConcatenanteNode(min, node);
            if (min == null || node.Key.CompareTo(min.Key) < 0)
            {
                min = node;
            }
            nodeCount++;
        }

        private FibHeapNode<T> ConcatenanteNode(FibHeapNode<T> list, FibHeapNode<T> node)
        {
            if (list == null)
            {
                node.LeftSibling = node;
                node.RightSibling = node;
                return node;
            }
            else
            {
                node.LeftSibling = list;
                node.RightSibling = list.RightSibling;
                list.RightSibling = node;
                node.RightSibling.LeftSibling = node;
                return list;
            }
        }

        public void Union(FibHeap<T> heap)
        {
            min = ConcatenateList(min, heap.min);
            if(min == null || (heap.min != null && min.Key.CompareTo(heap.min.Key) < 0))
            {
                min = heap.min;
            }
            nodeCount += heap.nodeCount;
        }

        private FibHeapNode<T> ConcatenateList(FibHeapNode<T> list1, FibHeapNode<T> list2)
        {
            if (list1 != null && list2 != null)
            {
                list1.RightSibling.LeftSibling = list2.LeftSibling;
                list2.LeftSibling.RightSibling = list1.RightSibling;
                list1.RightSibling = list2;
                list2.LeftSibling = list1;
                return list1;
            }
            else if (list1 == null)
            {
                return list2;
            }
            return list1;
        }

        public FibHeapNode<T> ExtractMinimum()
        {
            var z = min;
            if (z != null)
            {
                if (z.Child != null)
                {
                    foreach (var child in z.Child.SiblingsList)
                    {
                        min = ConcatenanteNode(min, child);
                        child.Parent = null;
                    }
                }
                min = RemoveNode(min, z);
                if (z == z.RightSibling)
                {
                    min = null;
                }
                else
                {
                    min = z.RightSibling;
                    Consolidate();
                }
                nodeCount--;
            }
            return z;
        }

        private void Consolidate()
        {
            int arraySize = (int)(Math.Floor(Math.Log(nodeCount) / Math.Log(phi))) + 1;
            var array = new FibHeapNode<T>[arraySize];
            if (min != null)
            {
                foreach (var node in min.SiblingsList)
                {
                    var x = node;
                    int d = x.Degree;
                    while (array[d] != null)
                    {
                        var y = array[d];
                        if (x.Key.CompareTo(y.Key) > 0)
                        {
                            var tmp = x;
                            x = y;
                            y = tmp;
                        }
                        Link(y, x);
                        array[d] = null;
                    }
                    array[d] = x;
                }
            }
            min = null;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    min = ConcatenanteNode(min, array[i]);
                    if (min == null || array[i].Key.CompareTo(min.Key) < 0)
                    {
                        min = array[i];
                    }
                }
            }
        }

        private void Link(FibHeapNode<T> y, FibHeapNode<T> x)
        {
            min = RemoveNode(min, y);
            y.Parent = x;
            y.RightSibling = y;
            y.LeftSibling = y;
            x.Child = ConcatenanteNode(x.Child, y);
            x.Degree++;
            y.Mark = false;
        }

        private FibHeapNode<T> RemoveNode(FibHeapNode<T> list, FibHeapNode<T> node)
        {
            if (node.LeftSibling == node)
            {
                return null;
            }
            node.LeftSibling.RightSibling = node.RightSibling;
            node.RightSibling.LeftSibling = node.LeftSibling;
            return list;
        }

        public void DecreaseKey(FibHeapNode<T> node, T key)
        {
            if (key.CompareTo(node.Key) > 0)
            {
                throw new Exception("new key cant be larger than previous key");
            }
            node.Key = key;
            var y = node.Parent;
            if (y != null && node.Key.CompareTo(y.Key) < 0)
            {
                Cut(node, y);
                CascadingCut(y);
            }
            if (node.Key.CompareTo(min.Key) == 0)
            {
                min = node;
            }
        }

        private void Cut(FibHeapNode<T> x, FibHeapNode<T> y)
        {
            y.Child = RemoveNode(y.Child, x);
            y.Degree--;
            min = ConcatenanteNode(min, x);
            x.Parent = null;
            x.Mark = false;
        }

        private void CascadingCut(FibHeapNode<T> y)
        {
            var z = y.Parent;
            if (z != null)
            {
                if (!y.Mark)
                {
                    y.Mark = true;
                }
                else
                {
                    Cut(y, z);
                    CascadingCut(z);
                }
            }
        }

        public void Delete(FibHeapNode<T> node)
        {
            //works because decrease key will update min if the previous min and the new one have the same key
            DecreaseKey(node, min.Key);
            ExtractMinimum();
        }
    }
}
