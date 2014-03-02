using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class VertexBFS<T> : IVertex<T>
    {
        public Color Color { get; set; }
        public IVertex<T> Predecessor { get; set; }
        public T Data { get; set; }
        public int DistanceToSource { get; set; }

        public VertexBFS(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString() + ", distance: " + DistanceToSource;
        }
    }
}
