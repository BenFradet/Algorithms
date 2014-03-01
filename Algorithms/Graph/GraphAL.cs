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
        private Dictionary<T, List<IVertex<T>>> edges;
        private IVertex<T> source;

        private int time;

        public GraphAL(IVertex<T>[] vertices, Dictionary<T, List<IVertex<T>>> edges, IVertex<T> source)
        {
            if (!vertices.Contains(source))
            {
                throw new Exception("not gonna work");
            }
            this.vertices = vertices;
            this.edges = edges;
            this.source = source;
        }

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
                foreach (var adjacentVertex in edges[vertex.Data])
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
                PrintPath(vertex.Predecessor);
                Console.WriteLine(vertex.ToString());
            }
        }

        public void DepthFirstSearch()
        {
            foreach (var vertex in vertices)
            {
                vertex.Color = Color.White;
                vertex.Predecessor = null;
            }
            time = 0;
        }

        private void DepthFirstVisit(VertexDFS<T> vertex)
        {
            time++;
            vertex.DiscoveryTime = time;
            vertex.Color = Color.Gray;
            foreach (var adjacentVertex in edges[vertex.Data])
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
        }
    }
}
