using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class VertexMST<T> : IVertex<T>, IComparable<VertexMST<T>>
    {
        public IVertex<T> Predecessor { get; set; }
        public T Data { get; set; }

        //height, number of edges in the longest path from a descendant leaf to x
        public int Rank { get; set; }
        //for prim's algorithm
        public int Key { get; set; }
        public IVertex<T> Parent { get; set; }

        public VertexMST(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString() + Key;
        }

        public int CompareTo(VertexMST<T> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
