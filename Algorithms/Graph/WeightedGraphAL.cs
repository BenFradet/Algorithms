using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class WeightedGraphAL<T>
    {
        private List<IVertex<T>> vertices;
        public List<IVertex<T>> Vertices { get { return vertices; } }
        private List<Edge<T>> edges;
        public List<Edge<T>> Edges { get { return edges; } }

        public WeightedGraphAL(List<IVertex<T>> vertices, List<Edge<T>> edges)
        {
            this.edges = edges;
            this.vertices = vertices;
        }

        public List<Edge<T>> MinimumSpanningTreeKruskal()
        {
            List<Edge<T>> spanningTree = new List<Edge<T>>();
            foreach (var vertex in vertices)
            {
                MakeSet(vertex);
            }
            edges = edges.OrderBy((e) => e.Weight).ToList();
            foreach (var edge in edges)
            {
                if (FindSet(edge.From) != FindSet(edge.To))
                {
                    spanningTree.Add(edge);
                    Union(edge.From, edge.To);
                }
            }
            return spanningTree;
        }

        private void MakeSet(IVertex<T> vertex)
        {
            (vertex as VertexMST<T>).Rank = 0;
            (vertex as VertexMST<T>).Parent = vertex;
        }

        private IVertex<T> FindSet(IVertex<T> vertex)
        {
            if (vertex != (vertex as VertexMST<T>).Parent)
            {
                (vertex as VertexMST<T>).Parent = FindSet((vertex as VertexMST<T>).Parent);
            }
            return (vertex as VertexMST<T>).Parent;
        }

        private void Union(IVertex<T> x, IVertex<T> y)
        {
            Link(FindSet(x), FindSet(y));
        }

        private void Link(IVertex<T> x, IVertex<T> y)
        {
            if ((x as VertexMST<T>).Rank > (y as VertexMST<T>).Rank)
            {
                (y as VertexMST<T>).Parent = x;
            }
            else
            {
                (x as VertexMST<T>).Parent = y;
                if ((x as VertexMST<T>).Rank == (y as VertexMST<T>).Rank)
                {
                    (y as VertexMST<T>).Rank++;
                }
            }
        }

        public void MinimumSpanningTreePrim(IVertex<T> root)
        {
            foreach (var vertex in vertices)
            {
                (vertex as VertexMST<T>).Key = int.MaxValue;
                vertex.Predecessor = null;
            }
            (root as VertexMST<T>).Key = 0;
            var array = vertices.OfType<VertexMST<T>>().ToArray();
            var heap = new MinHeap<VertexMST<T>>(array.Count(), array);
            //pretty careless
            while (!heap.IsEmpty)
            {
                //had to call heapify before extract min since it's not a min heap anymore due to the change of keys
                //instead of decrease key
                heap.Heapify(0);
                var min = heap.Extract();
                foreach (var edge in edges.Where((e) => e.From.Equals(min) || e.To.Equals(min)))
                {
                    if (edge.To.Equals(min))
                    {
                        if (heap.Contains(edge.From as VertexMST<T>) 
                            && edge.Weight < (edge.From as VertexMST<T>).Key)
                        {
                            edge.From.Predecessor = min;
                            (edge.From as VertexMST<T>).Key = edge.Weight;
                        }
                    }
                    else
                    {
                        if (heap.Contains(edge.To as VertexMST<T>) 
                            && edge.Weight < (edge.To as VertexMST<T>).Key)
                        {
                            edge.To.Predecessor = min;
                            (edge.To as VertexMST<T>).Key = edge.Weight;
                        }
                    }
                }
            }
        }
    }
}
