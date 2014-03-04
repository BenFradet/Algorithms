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
            IVertex<char> s = new VertexSSSP<char>('s');
            IVertex<char> t = new VertexSSSP<char>('t');
            IVertex<char> x = new VertexSSSP<char>('x');
            IVertex<char> y = new VertexSSSP<char>('y');
            IVertex<char> z = new VertexSSSP<char>('z');
            var vertices = new List<IVertex<char>>() { s, t, x, y, z };
            var edges = new List<Edge<char>>() { new Edge<char>(s, t, 6), new Edge<char>(s, y, 7), 
                new Edge<char>(t, x, 5), new Edge<char>(t, z, -4), new Edge<char>(t, y, 8), new Edge<char>(y, x, -3), 
                new Edge<char>(y, z, 9), new Edge<char>(x, t, -2), new Edge<char>(z, x, 7), new Edge<char>(z, s, 2) };
            var graph = new WeightedGraphAL<char>(vertices, edges);
            if (graph.BellmanFord(s))
            {
                foreach (var vertex in graph.Vertices)
                {
                    Console.WriteLine(vertex);
                }
            }
            #region comment
            //IVertex<char> a = new VertexMST<char>('a');
            //IVertex<char> b = new VertexMST<char>('b');
            //IVertex<char> c = new VertexMST<char>('c');
            //IVertex<char> d = new VertexMST<char>('d');
            //IVertex<char> e = new VertexMST<char>('e');
            //IVertex<char> f = new VertexMST<char>('f');
            //IVertex<char> g = new VertexMST<char>('g');
            //IVertex<char> h = new VertexMST<char>('h');
            //IVertex<char> i = new VertexMST<char>('i');
            //var vertices = new List<IVertex<char>>() { a, b, c, d, e, f, g, h, i };
            //var edges = new List<Edge<char>>() { new Edge<char>(a, b, 4), new Edge<char>(a, h, 9), 
            //    new Edge<char>(b, h, 11), new Edge<char>(b, c, 8), new Edge<char>(h, i, 7), new Edge<char>(h, g, 1),
            //    new Edge<char>(i, c, 2), new Edge<char>(c, d, 7), new Edge<char>(c, f, 4), new Edge<char>(g, f, 2),
            //    new Edge<char>(d, e, 9), new Edge<char>(d, f, 14), new Edge<char>(e, f, 10) };
            //var graph = new WeightedGraphAL<char>(vertices, edges);
            //graph.MinimumSpanningTreePrim(a as VertexMST<char>);
            //foreach (var edge in graph.Edges
            //    .Where((ed) => ed.To.Equals(ed.From.Predecessor) || ed.From.Equals(ed.To.Predecessor)))
            //{
            //    Console.WriteLine(edge);
            //}
            //foreach (var edge in graph.MinimumSpanningTreeKruskal())
            //{
            //    Console.WriteLine(edge);
            //}
            //IVertex<char> a = new VertexDFS<char>('a');
            //IVertex<char> b = new VertexDFS<char>('b');
            //IVertex<char> c = new VertexDFS<char>('c');
            //IVertex<char> d = new VertexDFS<char>('d');
            //IVertex<char> e = new VertexDFS<char>('e');
            //IVertex<char> f = new VertexDFS<char>('f');
            //IVertex<char> g = new VertexDFS<char>('g');
            //IVertex<char> h = new VertexDFS<char>('h');
            //var vertices = new IVertex<char>[] { c, g, f, h, d, b, e, a };
            //var edges = new Dictionary<IVertex<char>, List<IVertex<char>>>();
            //edges[a] = new List<IVertex<char>>() { b };
            //edges[b] = new List<IVertex<char>>() { e, f, c };
            //edges[c] = new List<IVertex<char>>() { d, g };
            //edges[d] = new List<IVertex<char>>() { c, h };
            //edges[e] = new List<IVertex<char>>() { a, f };
            //edges[f] = new List<IVertex<char>>() { g };
            //edges[g] = new List<IVertex<char>>() { f, h };
            //edges[h] = new List<IVertex<char>>() { h };
            //var graph = new GraphAL<char>(vertices, edges);
            //var dict = graph.StronglyConnectedComponents();
            //IVertex<char> a = new VertexDFS<char>('a');
            //IVertex<char> b = new VertexDFS<char>('b');
            //IVertex<char> c = new VertexDFS<char>('c');
            //var vertices = new IVertex<char>[] { a, b, c };
            //var edges = new Dictionary<IVertex<char>, List<IVertex<char>>>();
            //edges[a] = new List<IVertex<char>>() { b, c };
            //var graph = new GraphAL<char>(vertices, edges);
            //foreach (var vertex in graph.Edges)
            //{
            //    Console.WriteLine(vertex.Key.Data + ":");
            //    foreach (var vert in vertex.Value)
            //    {
            //        Console.WriteLine("\t" + vert.Data);
            //    }
            //}
            //var graph2 = graph.Transpose();
            //Console.WriteLine();
            //foreach (var vertex in graph2.Edges)
            //{
            //    Console.WriteLine(vertex.Key.Data + ":");
            //    foreach (var vert in vertex.Value)
            //    {
            //        Console.WriteLine("\t" + vert.Data);
            //    }
            //}
            //IVertex<string> undeshorts = new VertexDFS<string>("undershorts");
            //IVertex<string> pants = new VertexDFS<string>("pants");
            //IVertex<string> belt = new VertexDFS<string>("belt");
            //IVertex<string> jacket = new VertexDFS<string>("jacket");
            //IVertex<string> shirt = new VertexDFS<string>("shirt");
            //IVertex<string> tie = new VertexDFS<string>("tie");
            //IVertex<string> socks = new VertexDFS<string>("socks");
            //IVertex<string> shoes = new VertexDFS<string>("shoes");
            //IVertex<string> watch = new VertexDFS<string>("watch");
            //var vertices = new IVertex<string>[] { shirt, tie, jacket, belt, watch, undeshorts, pants, shoes, socks };
            //var edges = new Dictionary<IVertex<string>, List<IVertex<string>>>();
            //edges[undeshorts] = new List<IVertex<string>>() { pants, shoes };
            //edges[pants] = new List<IVertex<string>>() { shoes, belt };
            //edges[belt] = new List<IVertex<string>>() { jacket };
            //edges[shirt] = new List<IVertex<string>>() { tie, belt };
            //edges[tie] = new List<IVertex<string>>() { jacket };
            //edges[jacket] = new List<IVertex<string>>();
            //edges[socks] = new List<IVertex<string>>() { shoes };
            //edges[shoes] = new List<IVertex<string>>();
            //edges[watch] = new List<IVertex<string>>();
            //var graph = new GraphAL<string>(vertices, edges);
            //graph.DepthFirstSearch();
            //var list = graph.TopologicalSort();
            //foreach (var vertex in list)
            //{
            //    Console.WriteLine(vertex);
            //}
            //IVertex<char> r = new VertexBFS<char>('r');
            //IVertex<char> s = new VertexBFS<char>('s');
            //IVertex<char> t = new VertexBFS<char>('t');
            //IVertex<char> u = new VertexBFS<char>('u');
            //IVertex<char> v = new VertexBFS<char>('v');
            //IVertex<char> w = new VertexBFS<char>('w');
            //IVertex<char> x = new VertexBFS<char>('x');
            //IVertex<char> y = new VertexBFS<char>('y');
            //var vertices = new IVertex<char>[] { r, s, t, u, v, w, x, y };
            //var edges = new Dictionary<char, List<IVertex<char>>>();
            //edges[r] = new List<IVertex<char>>() { s, v };
            //edges[s] = new List<IVertex<char>>() { r, w };
            //edges[t] = new List<IVertex<char>>() { w, x, u };
            //edges[u] = new List<IVertex<char>>() { t, x, y };
            //edges[v] = new List<IVertex<char>>() { r };
            //edges[w] = new List<IVertex<char>>() { s, t, x };
            //edges[x] = new List<IVertex<char>>() { w, t, u, y };
            //edges[y] = new List<IVertex<char>>() { u, x };
            //var graph = new GraphAL<char>(vertices, edges, s);
            //graph.BreadthFirstSearch();
            //graph.PrintPath(u as VertexBFS<char>);
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
