﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class VertexDFS<T> : IVertex<T>
    {
        public Color Color { get; set; }
        public VertexBFS<T> Predecessor { get; set; }
        public T Data { get; set; }
        public int DiscoveryTime { get; set; }
        public int FinishingTime { get; set; }

        public VertexDFS(T data)
        {
            Data = data;
        }
    }
}
