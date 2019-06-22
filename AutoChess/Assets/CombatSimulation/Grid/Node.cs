using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool Walkable;
    public Vector3 WorldPosition;
    public int GridX, GridY;

    private HeroMover gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<HeroMover>();
    }

    public void Init(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        Walkable = walkable;
        WorldPosition = worldPosition;
        GridX = gridX;
        GridY = gridY;
    }

    public Vector3 GetPosition()
    {
        return WorldPosition + new Vector3(0, 1f, 0);
    }

    public float DistanceToNode(Vector3 worldPosition)
    {
        return Vector3.Distance(worldPosition, WorldPosition);
    }

    private void OnMouseDown()
    {
        gameManager.PlaceHero();
    }
}