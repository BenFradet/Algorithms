using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Edge<T>
    {
        public IVertex<T> From { get; set; }
        public IVertex<T> To { get; set; }
        public int Weight { get; set; }

        public Edge(IVertex<T> from, IVertex<T> to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public override string ToString()
        {
            return "From: " + From.ToString() + ", To: " + To.ToString() + ", Weigth: " + Weight;
        }
    }
}
