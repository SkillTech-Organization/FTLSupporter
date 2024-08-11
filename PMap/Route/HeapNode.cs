using System;
using System.Collections.Generic;
using System.Text;

namespace PMapCore.Route
{
    public struct HeapNode
    {
        public HeapNode(int i, float w)
        {
            index = i;
            weight = w;

        }
        public int index;
        public float weight;
    }
}
