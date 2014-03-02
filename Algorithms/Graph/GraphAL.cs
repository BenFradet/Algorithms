using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class GraphAL<T>
    {
        private IVertex<T>[] vertices;
        public IVertex<T>[] Vertices { get { return vertices; } }
        private Dictionary<IVertex<T>, List<IVertex<T>>> edges;
        public Dictionary<IVertex<T>, List<IVertex<T>>> Edges { get { return edges; } }
        private IVertex<T> source;

        private int time;
        private LinkedList<IVertex<T>> topologicallySortedVertices;

        public GraphAL(IVertex<T>[] vertices, Dictionary<IVertex<T>, List<IVertex<T>>> edges)
            : this(vertices, edges, null) { }

        public GraphAL(IVertex<T>[] vertices, Dictionary<IVertex<T>, List<IVertex<T>>> edges, IVertex<T> source)
        {
            //needed for breadth first
            if (source != null && !vertices.Contains(source))
            {
                throw new Exception("not gonna work");
            }
            this.vertices = vertices;
            this.edges = edges;
            this.source = source;

            topologicallySortedVertices = new LinkedList<IVertex<T>>();
        }

        /// <summary>
        /// uses breadth first vertices
        /// </summary>
        public void BreadthFirstSearch()
        {
            foreach (var vertex in vertices.Except(new List<IVertex<T>>() { source }))
            {
                vertex.Color = Color.White;
                vertex.Predecessor = null;
                (vertex as VertexBFS<T>).DistanceToSource = int.MaxValue;
            }
            source.Color = Color.Gray;
            source.Predecessor = null;
            (source as VertexBFS<T>).DistanceToSource = 0;
            var queue = new Queue<IVertex<T>>(vertices.Length);
            queue.Enqueue(source);
            while (!queue.IsEmpty)
            {
                var vertex = queue.Dequeue();
                foreach (var adjacentVertex in edges[vertex])
                {
                    if (adjacentVertex.Color == Color.White)
                    {
                        adjacentVertex.Color = Color.Gray;
                        adjacentVertex.Predecessor = vertex;
                        (adjacentVertex as VertexBFS<T>).DistanceToSource =
                            (vertex as VertexBFS<T>).DistanceToSource + 1;
                        queue.Enqueue(adjacentVertex);
                    }
                }
                vertex.Color = Color.Black;
            }
        }

        public void PrintPath(VertexBFS<T> vertex)
        {
            if (vertex.Equals(source))
            {
                Console.WriteLine(source.ToString());
            }
            else if (vertex.Predecessor == null)
            {
                Console.WriteLine("no path from source to vertex exists");
            }
            else
            {
                PrintPath(vertex.Predecessor as VertexBFS<T>);
                Console.WriteLine(vertex.ToString());
            }
        }

        /// <summary>
        /// Uses depth first vertices
        /// </summary>
        public void DepthFirstSearch()
        {
            foreach (var vertex in vertices)
            {
                vertex.Color = Color.White;
                vertex.Predecessor = null;
            }
            time = 0;
            foreach (var vertex in vertices)
            {
                if (vertex.Color == Color.White)
                {
                    DepthFirstVisit(vertex as VertexDFS<T>);
                }
            }
        }

        private void DepthFirstVisit(VertexDFS<T> vertex)
        {
            time++;
            vertex.DiscoveryTime = time;
            vertex.Color = Color.Gray;
            foreach (var adjacentVertex in edges[vertex])
            {
                if (adjacentVertex.Color == Color.White)
                {
                    adjacentVertex.Predecessor = vertex;
                    DepthFirstVisit(adjacentVertex as VertexDFS<T>);
                }
            }
            vertex.Color = Color.Black;
            time++;
            vertex.FinishingTime = time;
            topologicallySortedVertices.AddFirst(vertex);
        }

        private void DepthFirstSearchOrdered(LinkedList<IVertex<T>> sortedByFinishingTime)
        {
            foreach (var vertex in vertices)
            {
                vertex.Color = Color.White;
                vertex.Predecessor = null;
            }
            time = 0;
            foreach (var vertex in sortedByFinishingTime)
            {
                if (vertex.Color == Color.White)
                {
                    DepthFirstVisit(vertex as VertexDFS<T>);
                }
            }
        }

        /// <summary>
        /// The graph has to be directed and acyclic
        /// </summary>
        public LinkedList<IVertex<T>> TopologicalSort()
        {
            if (topologicallySortedVertices.Count == 0)
            {
                DepthFirstSearch();
            }
            return topologicallySortedVertices;
        }

        public Dictionary<IVertex<T>, List<IVertex<T>>> StronglyConnectedComponents()
        {
            DepthFirstSearch();
            var transposed = Transpose();
            transposed.DepthFirstSearchOrdered(topologicallySortedVertices);

            var dict = new Dictionary<IVertex<T>, List<IVertex<T>>>();
            //dict.Keys = transposed.vertices.Where((v) => v.Predecessor == null);
            foreach (var vertex in transposed.vertices)
            {
                if (vertex.Predecessor == null)
                {
                    dict[vertex] = new List<IVertex<T>>();
                }
            }
            foreach (var vertex in transposed.vertices)
            {
                if (vertex.Predecessor != null)
                {
                    var list = new List<IVertex<T>>();
                    var tmp = vertex;
                    do
                    {
                        list.Add(tmp);
                        tmp = tmp.Predecessor;
                    } while (!dict.ContainsKey(tmp));
                    if (dict[tmp].Count < list.Count)
                    {
                        dict[tmp] = list;
                    }
                }
            }
            return dict;
        }

        public GraphAL<T> Transpose()
        {
            var tmpEdges = new Dictionary<IVertex<T>, List<IVertex<T>>>();
            foreach (var edge in edges)
            {
                foreach (var vertex in edge.Value)
                {
                    if (!tmpEdges.ContainsKey(vertex))
                    {
                        tmpEdges[vertex] = new List<IVertex<T>>();
                    }
                    tmpEdges[vertex].Add(edge.Key);
                }
            }
            return new GraphAL<T>(vertices, tmpEdges);
        }
    }
}
