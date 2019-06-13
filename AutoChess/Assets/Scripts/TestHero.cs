using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestHero : MonoBehaviour
{
    public MeshRenderer rend { get; set; }
    public Color standardColor { get; private set; }

    private HeroMover gameManager;
    private NodeGrid grid;

    private void Awake()
    {
        rend = GetComponentInChildren<MeshRenderer>();
        gameManager = FindObjectOfType<HeroMover>();
        grid = FindObjectOfType<NodeGrid>();
    }

    public void SetHeroProperties(Color color)
    {
        standardColor = color;
        rend.material.color = color;
    }

    int currentPos;
    int targetPos;
    Node targetNode;
    Vector3 targetMovePos;
    bool showDirection = false;

    public void CalculateMove(List<Node> enemyNodes)
    {
        FindTargetTile(enemyNodes);

        targetMovePos = targetNode.WorldPosition;
        showDirection = true;
    }

    private bool FindTargetTile(List<Node> enemyNodes)
    {
        List<Node> potentialTargets = enemyNodes;

        while (potentialTargets.Count > 0)
        {
            List<Node> sortedTargetNodes = SortClosestNode(potentialTargets);

            //
            if (GetClosestAvailableTile(targetHeroTile, out int availableTile))
            {
                SetNewTarget(availableTile);
                return true;
            }
            else
                potentialTargets.Remove(targetHeroNode);
        }
        return false;
    }

    private List<Node> SortClosestNode(List<Node> enemyNodes)
    {
        List<Node> closestNodes = enemyNodes.OrderBy(o => o.DistanceToNode(transform.position)).ToList();
        return closestNodes;
    }

    private float DistanceToNode(Node node)
    {
        return Vector3.Distance(transform.position, node.WorldPosition);
    }

    public void Move()
    {
        showDirection = false;
        transform.position = targetMovePos;
    }

    private int CheckDif(int dif)
    {
        if (dif < 0)
            return -1;
        else if (dif == 0)
            return 0;
        else
            return 1;
    }

    private void OnMouseDown()
    {
        gameManager.SelectHero(this);
    }

    private void OnDrawGizmos()
    {
        if(showDirection)
            Gizmos.DrawLine(transform.position, targetMovePos);
    }
}