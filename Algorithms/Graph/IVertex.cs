using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public interface IVertex<T>
    {
        IVertex<T> Predecessor { get; set; }
        T Data { get; set; }
    }
}
