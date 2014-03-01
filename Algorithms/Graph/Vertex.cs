using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Vertex<T>
    {
        public Color Color { get; set; }
        public Vertex<T> Predecessor { get; set; }
        public int DistanceToSource { get; set; }
        public T Data { get; set; }

        public Vertex(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString() + " distance: " + DistanceToSource + ", color: " + Color.ToString();
        }
    }
}
