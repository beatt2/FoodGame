using System;
using Node;
using UnityEngine;

namespace Grid
{
    public class SingleGridList : IComparable<SingleGridList>
    {
        public NodeBehaviour Node{ get; private set; }
        private Vector2Int GridLocations { get; set; }


        public int CompareTo(SingleGridList other)
        {
            if (other == null)
            {
                return 1;
            }

            if (GridLocations.y > other.GridLocations.y)
            {
                return 1;
            }
            if (GridLocations.y < other.GridLocations.y)
            {
                return -1;
            }
            return 0;

        }

        public SingleGridList(NodeBehaviour node, Vector2Int gridLocations)
        {
            GridLocations = gridLocations;
            Node = node;
        }



    }
}
