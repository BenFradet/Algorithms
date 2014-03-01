using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class GraphAL<T>
    {
        private Vertex<T>[] vertices;
        private Dictionary<T, List<Vertex<T>>> edges;
        private Vertex<T> source;

        public GraphAL(Vertex<T>[] vertices, Dictionary<T, List<Vertex<T>>> edges, Vertex<T> source)
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
            foreach (var vertex in vertices.Except(new List<Vertex<T>>() { source }))
            {
                vertex.Color = Color.White;
                vertex.DistanceToSource = int.MaxValue;
                vertex.Predecessor = null;
            }
            source.Color = Color.Grey;
            source.DistanceToSource = 0;
            source.Predecessor = null;
            var queue = new Queue<Vertex<T>>(vertices.Length);
            queue.Enqueue(source);
            while (!queue.IsEmpty)
            {
                var vertex = queue.Dequeue();
                foreach (var adjacentVertex in edges[vertex.Data])
                {
                    if (adjacentVertex.Color == Color.White)
                    {
                        adjacentVertex.Color = Color.Grey;
                        adjacentVertex.DistanceToSource = vertex.DistanceToSource + 1;
                        adjacentVertex.Predecessor = vertex;
                        queue.Enqueue(adjacentVertex);
                    }
                }
                vertex.Color = Color.Black;
            }
        }

        public void PrintPath(Vertex<T> vertex)
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
    }
}
