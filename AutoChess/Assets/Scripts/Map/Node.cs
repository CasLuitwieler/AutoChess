using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector3 WorldPosition;
    public int GridX, GridY;

    public Node()
    {
        Walkable = false;
        WorldPosition = Vector3.zero;
        GridX = 0;
        GridY = 0;
    }

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        Walkable = walkable;
        WorldPosition = worldPosition;
        GridX = gridX;
        GridY = gridY;
    }

    public float DistanceToNode(Vector3 worldPosition)
    {
        return Vector3.Distance(worldPosition, WorldPosition);
    }
}
