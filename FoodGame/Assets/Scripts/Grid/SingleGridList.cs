
using System;
using System.Collections;
using Node;
using UnityEngine;

public class SingleGridList : IComparable<SingleGridList>
{
    public NodeBehaviour Node{ get; private set; }
    public Vector2Int GridLocations { get; private set; }


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
