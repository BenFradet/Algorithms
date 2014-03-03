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
        private List<Edge<T>> edges;

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
    }
}
