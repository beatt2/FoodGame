using System;
using Node;
using UnityEngine;

namespace Grid
{
    public class GridList : IComparable<GridList>
    {
        public NodeBehaviour Node{ get; private set; }
        public Vector2Int GridLocations { get; private set; }


        public int CompareTo(GridList other)
        {
            if (other == null)
            {
                return 1;
            }

            if (GridLocations.x > other.GridLocations.x)
            {
                return 1;
            }
            if (GridLocations.x < other.GridLocations.x)
            {
                return -1;
            }
            return 0;

        }

        public GridList(NodeBehaviour node, Vector2Int gridLocations)
        {
            GridLocations = gridLocations;
            Node = node;
        }
        
        

    }
}
