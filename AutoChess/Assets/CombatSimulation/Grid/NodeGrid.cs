using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize = new Vector2(8, 8);
    public float NodeRadius = 0.5f;

    [SerializeField] private GameObject NodeGO = null;

    private Node[,] grid;
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = NodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask);
                Node node = Instantiate(NodeGO, worldPoint, Quaternion.identity, this.transform).GetComponent<Node>();
                node.Init(walkable, worldPoint, x, y);
                grid[x, y] = node;                
            }
        }
    }

    private void Update()
    {
        CheckWalkableNodes();
    }

    private void CheckWalkableNodes()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask);
                grid[x, y].Walkable = walkable;
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }

    public List<Node> GetCornerNeighbours(Node node, Corner corner)
    {
        List<Node> neighbours = new List<Node>();
        int x = node.GridX;
        int y = node.GridY;
        switch (corner)
        {
            case Corner.TopLeft:
                neighbours.Add(grid[x,      y + 1]);
                neighbours.Add(grid[x - 1,  y + 1]);
                neighbours.Add(grid[x - 1,  y]);
                break;
            case Corner.TopRight:
                neighbours.Add(grid[x,      y + 1]);
                neighbours.Add(grid[x + 1,  y + 1]);
                neighbours.Add(grid[x + 1,  y]);
                break;
            case Corner.BottomLeft:
                neighbours.Add(grid[x,      y - 1]);
                neighbours.Add(grid[x - 1,  y - 1]);
                neighbours.Add(grid[x - 1,  y]);
                break;
            case Corner.BottomRight:
                neighbours.Add(grid[x,      y - 1]);
                neighbours.Add(grid[x + 1,  y - 1]);
                neighbours.Add(grid[x + 1,  y]);
                break;
        }
        return neighbours;
    }

    public List<Node> GetCornerNeighbours(Node node, Side side)
    {
        List<Node> neighbours = new List<Node>();
        int x = node.GridX;
        int y = node.GridY;
        switch (side)
        {
            case Side.Top:
                neighbours.Add(grid[x - 1,  y + 1]);
                neighbours.Add(grid[x,      y + 1]);
                neighbours.Add(grid[x + 1,  y + 1]);
                break;
            case Side.Bottom:
                neighbours.Add(grid[x - 1,  y - 1]);
                neighbours.Add(grid[x,      y - 1]);
                neighbours.Add(grid[x + 1,  y - 1]);
                break;
            case Side.Left:
                neighbours.Add(grid[x - 1,  y + 1]);
                neighbours.Add(grid[x - 1,  y]);
                neighbours.Add(grid[x - 1,  y - 1]);
                break;
            case Side.Right:
                neighbours.Add(grid[x + 1,  y + 1]);
                neighbours.Add(grid[x + 1,  y]);
                neighbours.Add(grid[x + 1,  y - 1]);
                break;
        }
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;
        Mathf.Clamp01(percentX);
        Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
        Gizmos.color = Color.yellow;

        if (grid != null)
        {
            foreach(Node node in grid)
            {
                if(!node.Walkable)
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

public enum Corner
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    None
}

public enum Side
{
    Top,
    Bottom,
    Left,
    Right,
    None
}
