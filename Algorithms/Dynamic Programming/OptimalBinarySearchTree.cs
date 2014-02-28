using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class OptimalBinarySearchTree<T> where T : IComparable<T>
    {
        //probability that we search for key i
        private double[] probabilities;
        //unsuccessful searches, probability that the value we're looking for is less than i
        private double[] notFoundProbabilities;

        private T[] keys;
        
        //expected search costs in the subtree containing the values having keys between i and j
        private double[,] costs;
        //sum of probas
        private double[,] weights;
        //gives the index r for which kr is the root of the subtree containing keys between i and j
        private int[,] roots;

        private const int MAX_SIZE = 8;

        public OptimalBinarySearchTree()
        {
            probabilities = new double[] { 0, 0.15, 0.10, 0.05, 0.10, 0.20 };
            notFoundProbabilities = new double[] { 0.05, 0.10, 0.05, 0.05, 0.05, 0.10 };
            keys = new T[MAX_SIZE];
        }

        public struct ExpectedCostsAndRootsAndWeights
        {
            public double[,] ExpectedCosts { get; set; }
            public int[,] Roots { get; set; }
            public double[,] Weighs { get; set; }
        }

        public ExpectedCostsAndRootsAndWeights OptimalBST(int size)
        {
            costs = new double[MAX_SIZE, MAX_SIZE];
            weights = new double[MAX_SIZE, MAX_SIZE];
            roots = new int[MAX_SIZE, MAX_SIZE];
            for (int i = 0; i <= size; i++)
            {
                weights[i, i] = notFoundProbabilities[i];
                for (int j = i + 1; j <= size; j++)
                {
                    weights[i, j] = weights[i, j - 1] + probabilities[j] + notFoundProbabilities[j];
                }
            }
            for (int i = 0; i <= size; i++)
            {
                costs[i, i] = weights[i, i];
            }
            for (int i = 0; i < size; i++)
            {
                int j = i + 1;
                costs[i, j] = costs[i, i] + costs[j, j] + weights[i, j];
                roots[i, j] = j;
            }
            for (int h = 2; h <= size; h++)
            {
                for (int i = 0; i <= size - h; i++)
                {
                    int j = i + h;
                    int m = roots[i, j - 1];
                    double min = costs[i, m - 1] + costs[m, j];
                    for (int k = m + 1; k <= roots[i + 1, j]; k++)
                    {
                        double x = costs[i, k - 1] + costs[k, j];
                        if (x < min)
                        {
                            m = k;
                            min = x;
                        }
                    }
                    costs[i, j] = weights[i, j] + min;
                    roots[i, j] = m;
                }
            }
            return new ExpectedCostsAndRootsAndWeights() { ExpectedCosts = costs, Roots = roots, Weighs = weights };
        }

        public class BSTNode<T> where T : IComparable<T>
        {
            public BSTNode<T> LeftChild { get; set; }
            public BSTNode<T> RightChild { get; set; }
            public T Key { get; set; }
        }

        public BSTNode<T> ConstructTree(int i, int j)
        {
            BSTNode<T> node;
            if (i == j)
            {
                node = null;
            }
            else
            {
                node = new BSTNode<T>()
                {
                    Key = keys[roots[i, j]],
                    LeftChild = ConstructTree(i, roots[i, j] - 1),
                    RightChild = ConstructTree(roots[i, j], j)
                };
            }
            return node;
        }

        public void Display(BSTNode<T> root, int level)
        {
            if (root != null)
            {
                Display(root.RightChild, level + 1);
                for (int i = 0; i <= level; i++)
                {
                    Console.WriteLine("     ");
                }
                Console.WriteLine(root.Key + "\n");
                Display(root.LeftChild, level + 1);
            }
        }
    }
}
