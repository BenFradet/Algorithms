using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class VertexSSSP<T> : IVertex<T>, IComparable<VertexSSSP<T>>
    {
        public IVertex<T> Predecessor { get; set; }
        public T Data { get; set; }

        public bool Visited { get; set; }
        public int ShortestPathEstimate { get; set; }

        public VertexSSSP(T data)
        {
            Data = data;
            Visited = false;
        }

        public override string ToString()
        {
            return Data.ToString() + ", shortest path estimate: " + ShortestPathEstimate;
        }

        public int CompareTo(VertexSSSP<T> other)
        {
            return ShortestPathEstimate.CompareTo(other.ShortestPathEstimate);
        }
    }
}
