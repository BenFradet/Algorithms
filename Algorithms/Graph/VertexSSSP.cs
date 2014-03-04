using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class VertexSSSP<T> : IVertex<T>
    {
        public IVertex<T> Predecessor { get; set; }
        public T Data { get; set; }

        public int ShortestPathEstimate { get; set; }

        public VertexSSSP(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString() + ", shortest path estimate: " + ShortestPathEstimate;
        }
    }
}
