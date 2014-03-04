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

        #region minimum spanning trees
        public List<Edge<T>> MinimumSpanningTreeKruskal()
        {
            List<Edge<T>> spanningTree = new List<Edge<T>>();
            foreach (var vertex in vertices.OfType<VertexMST<T>>())
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

        private void MakeSet(VertexMST<T> vertex)
        {
            vertex.Rank = 0;
            vertex.Parent = vertex;
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

        public void MinimumSpanningTreePrim(VertexMST<T> root)
        {
            foreach (var vertex in vertices.OfType<VertexMST<T>>())
            {
                vertex.Key = int.MaxValue;
                vertex.Predecessor = null;
            }
            root.Key = 0;
            var heap = new MinHeap<VertexMST<T>>(vertices.Count, vertices.OfType<VertexMST<T>>().ToArray());
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
        #endregion

        #region single source shortest paths
        private void Initialize(IVertex<T> source)
        {
            if (!vertices.Contains(source))
            {
                throw new InvalidOperationException("source has to be a vertex contained in the graph");
            }
            foreach (var vertex in vertices.OfType<VertexSSSP<T>>())
            {
                vertex.ShortestPathEstimate = int.MaxValue;
                vertex.Predecessor = null;
            }
            (source as VertexSSSP<T>).ShortestPathEstimate = 0;
        }

        private void Relax(IVertex<T> u, IVertex<T> v, int weight)
        {
            if ((v as VertexSSSP<T>).ShortestPathEstimate > (u as VertexSSSP<T>).ShortestPathEstimate + weight)
            {
                (v as VertexSSSP<T>).ShortestPathEstimate = (u as VertexSSSP<T>).ShortestPathEstimate + weight;
                v.Predecessor = u;
            }
        }

        private bool RelaxWithoutUpdatingPath(IVertex<T> u, IVertex<T> v, int weight)
        {
            if ((v as VertexSSSP<T>).ShortestPathEstimate > (u as VertexSSSP<T>).ShortestPathEstimate + weight)
            {
                v.Predecessor = u;
                return true;
            }
            return false;
        }

        //can have negative cycles
        public bool BellmanFord(IVertex<T> source)
        {
            Initialize(source);
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                foreach (var edge in edges)
                {
                    Relax(edge.From, edge.To, edge.Weight);
                }
            }
            foreach (var edge in edges)
            {
                if ((edge.To as VertexSSSP<T>).ShortestPathEstimate > 
                    (edge.From as VertexSSSP<T>).ShortestPathEstimate + edge.Weight)
                {
                    return false;
                }
            }
            return true;
        }

        //edges' weights cannot be negative
        public void Dijkstra(IVertex<T> source)
        {
            Initialize(source);
            //var set = new List<VertexSSSP<T>>();
            var heap = new MinHeap<VertexSSSP<T>>(vertices.Count + edges.Count);
            heap.Insert(source as VertexSSSP<T>);
            while (!heap.IsEmpty)
            {
                var vertex = heap.Extract();
                if (vertex.Visited)
                {
                    continue;
                }
                vertex.Visited = true;
                //set.Add(node.Key);
                foreach (var edge in edges.Where((e) => e.From.Equals(vertex)))
                {                    
                    Relax(edge.From, edge.To, edge.Weight);
                    heap.Insert(edge.To as VertexSSSP<T>);
                }
            }
        }
        #endregion
    }
}
