using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class Program
    {
        static int heapSize;

        static void Main(string[] args)
        {
            var r = new Vertex<char>('r');
            var s = new Vertex<char>('s');
            var t = new Vertex<char>('t');
            var u = new Vertex<char>('u');
            var v = new Vertex<char>('v');
            var w = new Vertex<char>('w');
            var x = new Vertex<char>('x');
            var y = new Vertex<char>('y');
            var vertices = new Vertex<char>[] { r, s, t, u, v, w, x, y };
            var edges = new Dictionary<char, List<Vertex<char>>>();
            edges['r'] = new List<Vertex<char>>() { s, v };
            edges['s'] = new List<Vertex<char>>() { r, w };
            edges['t'] = new List<Vertex<char>>() { w, x, u };
            edges['u'] = new List<Vertex<char>>() { t, x, y };
            edges['v'] = new List<Vertex<char>>() { r };
            edges['w'] = new List<Vertex<char>>() { s, t, x };
            edges['x'] = new List<Vertex<char>>() { w, t, u, y };
            edges['y'] = new List<Vertex<char>>() { u, x };
            var graph = new GraphAL<char>(vertices, edges, s);
            graph.BreadthFirstSearch();
            graph.PrintPath(u);
            #region comment
            //var mat = new MatrixChainMult();
            //int nr = mat.MemoizedMatrixChain();
            //var lcs = new LongestCommonSubsequence<char>(new char[] { 'a', 'b', 'c', 'b', 'd', 'a', 'b' }
            //    , new char[] { 'b', 'd', 'c', 'a', 'b', 'a' });
            //var result = lcs.LCSLength();
            //lcs.PrintLCS(7, 6);
            //var optSearchTree = new OptimalBinarySearchTree();
            //var result = optSearchTree.OptimalBST(5);
            //var matrixChainMult = new MatrixChainMult();
            //var result = matrixChainMult.MatrixChainOrder();
            //matrixChainMult.PrintOptimal(result.OptimalCosts, 0, result.OptimalCosts.GetLength(0) - 1);
            //var rodCutting = new RodCutting();
            //rodCutting.PrintBottomUpCutRodWithSolution(10);
            //RedBlackTree tree = new RedBlackTree();
            //tree.Insert(new RedBlackTreeNode(12));
            //tree.Insert(new RedBlackTreeNode(5));
            //tree.Insert(new RedBlackTreeNode(18));
            //tree.Insert(new RedBlackTreeNode(2));
            //tree.Insert(new RedBlackTreeNode(9));
            //tree.Insert(new RedBlackTreeNode(1));
            //tree.InOrderTreeWalk(tree.Root);
            //Console.WriteLine(tree.Root.Key + " left" + tree.Root.LeftChild.Key + " right" + tree.Root.RightChild.Key);
            //Console.WriteLine(tree.Root.LeftChild.LeftChild.Key);
            //BinarySearchTree tree = new BinarySearchTree();
            //tree.Insert(new BinarySearchTreeNode(12));
            //tree.Insert(new BinarySearchTreeNode(5));
            //tree.Insert(new BinarySearchTreeNode(18));
            //tree.Insert(new BinarySearchTreeNode(2));
            //tree.Insert(new BinarySearchTreeNode(9));
            //tree.InOrderTreeWalk(tree.Root);
            //tree.Delete(tree.Root.LeftChild);
            //tree.InOrderTreeWalk(tree.Root);
            //int[] input = new int[] { 8, 2, 5, 1, 9, 4, 3 };
            //float[] floats = new float[] { 0.10f, 0.58f, 0.32f, 0.98f, 0.08f, 0.44f, 0.72f, 0.24f, 0.68f, 0.8f };
            //float[] result = BucketSortClass.BucketSort(floats);
            #endregion
            Console.ReadLine();
        }

        static void HeapSort(int[] input)
        {
            BuildMaxHeap(input);
            for (int i = input.Length - 1; i > 0; i--)
            {
                Swap(input, 0, i);
                MaxHeapify(input, 0);
            }
        }

        static void BuildMaxHeap(int[] input)
        {
            heapSize = input.Length;
            for (int i = input.Length / 2; i >= 0; i++)
                MaxHeapify(input, i);
        }

        static void MaxHeapify(int[] input, int i)
        {
            int left = i * 2 + 1;
            int right = (i + 1) * 2;
            int largest = int.MinValue;
            if (left < heapSize && input[left] > input[i])
                largest = left;
            else
                largest = i;
            if (right < heapSize && input[right] > input[i])
                largest = right;

            if (largest != i)
            {
                Swap(input, i, largest);
                MaxHeapify(input, largest);
            }
        }

        static int[] CountingSort(int[] a, int k)
        {
            int[] c = new int[k];
            for (int i = 0; i < a.Length; i++)
                c[a[i]]++;
            for (int i = 1; i < k; i++)
                c[i] += c[i - 1];
            int[] b = new int[a.Length];
            for (int i = a.Length - 1; i >= 0; i--)
                b[--c[a[i]]] = a[i];
            return b;
        }

        static void QuickSort(int[] input, int p, int r)
        {
            if (p < r)
            {
                int q = Partition(input, p, r);
                QuickSort(input, p, q - 1);
                QuickSort(input, q + 1, r);
            }
        }

        static int Partition(int[] input, int p, int r)
        {
            int x = input[r];
            int j = p - 1;
            for (int i = p; i < r; i++)
            {
                if (input[i] <= x)
                {
                    j++;
                    Swap(input, i, j);
                }
            }
            Swap(input, j + 1, r);
            return j + 1;
        }

        static void Swap(int[] input, int index1, int index2)
        {
            int tmp = input[index1];
            input[index1] = input[index2];
            input[index2] = tmp;
        }
    }
}
