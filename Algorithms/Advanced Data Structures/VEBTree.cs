using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class VEBTree
    {
        private VEBTree[] cluster;
        private VEBTree summary;
        private int universeSize;
        private int? min;
        private int? max;

        public VEBTree(int size)
        {
            if (size < 0 || (size & (size - 1)) != 0)
            {
                throw new Exception("The size has to be a power of 2 and positive");
            }
            universeSize = size;
            if (universeSize > 1)
            {
                int upperSqrt = UpperSqrt(universeSize);
                summary = new VEBTree(upperSqrt);
                cluster = new VEBTree[upperSqrt];
                for (int i = 0; i < upperSqrt; i++)
                {
                    cluster[i] = new VEBTree(LowerSqrt(universeSize));
                }
            }
        }

        public int? Minimum()
        {
            return min;
        }

        private int? MinimumImpl(VEBTree tree)
        {
            return tree.min;
        }

        public int? Maximum()
        {
            return max;
        }

        private int? MaximumImpl(VEBTree tree)
        {
            return tree.max;
        }

        public bool Member(int x)
        {
            return MemberImpl(this, x);
        }

        private bool MemberImpl(VEBTree tree, int x)
        {
            if (x == tree.min || x == tree.max)
            {
                return true;
            }
            else if (tree.universeSize == 2)
            {
                return false;
            }
            return MemberImpl(tree.cluster[HighBits(x)], LowBits(x));
        }

        public int? Successor(int x)
        {
            return SuccessorImpl(this, x);
        }

        private int? SuccessorImpl(VEBTree tree, int x)
        {
            if (tree.universeSize == 2)
            {
                if (x == 0 && tree.max == 1)
                {
                    return 1;
                }
                else
                {
                    return null;
                }
            }
            else if (tree.min != null && x < tree.min)
            {
                return tree.min;
            }
            else
            {
                var maxLow = MaximumImpl(tree.cluster[HighBits(x)]);
                if (maxLow != null && LowBits(x) < maxLow.Value)
                {
                    var offset = SuccessorImpl(tree.cluster[HighBits(x)], LowBits(x));
                    return Index(HighBits(x), offset.Value);
                }
                else
                {
                    var succCluster = SuccessorImpl(tree.summary, HighBits(x));
                    if (succCluster == null)
                    {
                        return null;
                    }
                    else
                    {
                        var offset = MinimumImpl(tree.cluster[succCluster.Value]);
                        return Index(succCluster.Value, offset.Value);
                    }
                }
            }
        }

        private int? PredecessorImpl(VEBTree tree, int x)
        {
            if (tree.universeSize == 2)
            {
                if (x == 1 && tree.min == 0)
                {
                    return 0;
                }
                else
                {
                    return null;
                }
            }
            else if (tree.max != null && x > tree.max)
            {
                return tree.max;
            }
            else
            {
                var minLow = MinimumImpl(tree.cluster[HighBits(x)]);
                if (minLow != null && LowBits(x) > minLow)
                {
                    var offset = PredecessorImpl(tree.cluster[HighBits(x)], LowBits(x));
                    return Index(HighBits(x), offset.Value);
                }
                else
                {
                    var predCluster = PredecessorImpl(tree.summary, HighBits(x));
                    if (predCluster == null)
                    {
                        if (tree.min != null && x > tree.min)
                        {
                            return tree.min;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var offset = MaximumImpl(tree.cluster[predCluster.Value]);
                        return Index(predCluster.Value, offset.Value);
                    }
                }
            }
        }

        public void Insert(int x)
        {
            InsertImpl(this, x);
        }

        private void InsertImpl(VEBTree tree, int x)
        {
            if (tree.min == null)
            {
                EmptyTreeInsertImpl(tree, x);
            }
            else if (x < tree.min.Value)
            {
                var tmp = x;
                x = tree.min.Value;
                tree.min = tmp;
                if (tree.universeSize == 2)
                {
                    if (MinimumImpl(tree.cluster[HighBits(x)]) == null)
                    {
                        InsertImpl(tree.summary, HighBits(x));
                        EmptyTreeInsertImpl(tree.cluster[HighBits(x)], LowBits(x));
                    }
                    else
                    {
                        InsertImpl(tree.cluster[HighBits(x)], LowBits(x));
                    }
                }
                if (x > tree.max.Value)
                {
                    tree.max = x;
                }
            }
        }

        private void EmptyTreeInsertImpl(VEBTree tree, int x)
        {
            tree.min = x;
            tree.max = x;
        }

        public void Delete(int x)
        {
            DeleteImpl(this, x);
        }

        private void DeleteImpl(VEBTree tree, int x)
        {
            if (tree.min == tree.max)
            {
                min = max = null;
            }
            else if (tree.universeSize == 2)
            {
                if (x == 0)
                {
                    tree.min = 1;
                }
                else
                {
                    tree.min = 0;
                }
                tree.max = tree.min;
            }
            else 
            {
                if (x == tree.min)
                {
                    int firstCluster = MinimumImpl(tree.summary).Value;
                    x = Index(firstCluster, MinimumImpl(tree.cluster[firstCluster]).Value);
                    tree.min = x;
                }
                DeleteImpl(tree.cluster[HighBits(x)], LowBits(x));
                if (MinimumImpl(tree.cluster[HighBits(x)]) == null)
                {
                    DeleteImpl(tree.summary, HighBits(x));
                    if (x == tree.max)
                    {
                        var summaryMax = MaximumImpl(tree.summary);
                        if (summaryMax == null)
                        {
                            tree.max = tree.min;
                        }
                        else
                        {
                            tree.max = Index(summaryMax.Value, MaximumImpl(tree.cluster[summaryMax.Value]).Value);
                        }
                    }
                }
                else if (x == tree.max)
                {
                    tree.max = Index(HighBits(x), MaximumImpl(tree.cluster[HighBits(x)]).Value);
                }
            }
        }

        // returns the most significant lg(u) / 2 bits of x
        private int HighBits(int x)
        {
            return (int)Math.Floor((double)(x / LowerSqrt(universeSize)));
        }

        private int LowBits(int x)
        {
            return x % LowerSqrt(universeSize);
        }

        //constructs an element with x as most significant bits and y as least significant bits
        //x = Index(HighBits(x), LowBits(x))
        private int Index(int x, int y)
        {
            return x * LowerSqrt(universeSize) + y;
        }

        private int UpperSqrt(int x)
        {
            return (int)Math.Pow(2, Math.Ceiling(Math.Log(x) / 2));
        }

        private int LowerSqrt(int x)
        {
            return (int)Math.Pow(2, Math.Floor(Math.Log(x) / 2));
        }
    }
}
