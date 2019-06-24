using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TestHero
{
    public Transform transform { get; private set; }

    public Node targetNode = null;
    private Vector3 targetMovePos;
    private bool showDirection = false;

    private DebugUI debugLine;
    
    public TestHero(Transform transform)
    {
        this.transform = transform;
        debugLine = new DebugUI(transform.GetComponent<LineRenderer>());
    }

    public void CalculateMove(List<Node> enemyNodes)
    {
        FindTargetTile(enemyNodes);

        //targetMovePos = targetNode.WorldPosition;
        debugLine.UpdateLines(true);
        showDirection = true;
    }

    private bool FindTargetTile(List<Node> enemyNodes)
    {
        List<Node> potentialTargets = enemyNodes;

        List<Node> sortedTargetNodes = SortClosestNode(potentialTargets);
        RemoveFarNodes(out float closestDistance, ref sortedTargetNodes);

        foreach (Node node in sortedTargetNodes)
        {
            debugLine.AddPoint(node.GetPosition());
            //if xdiff == ydiff
                //get diagonal tiles
            //if xdiff <= 1
                //get horizontal tiles
            //if ydiff <= 1
                //get vertical tiles
        }
        /*
        while (potentialTargets.Count > 0)
        {
            if (GetClosestAvailableTile(targetHeroTile, out int availableTile))
            {
                SetNewTarget(availableTile);
                return true;
            }
            else
                potentialTargets.Remove(targetHeroNode);
            
        }
        */
        return false;
    }

    public void DrawLines()
    {
        /*
        if (showDirection)
            debugLine.UpdateLines(true);
        else
            debugLine.UpdateLines(false);
            */
    }

    private void RemoveFarNodes(out float closestDistance, ref List<Node> sortedTargetNodes)
    {
        closestDistance = sortedTargetNodes[0].DistanceToNode(transform.position);

        for (int i = sortedTargetNodes.Count - 1; i >= 0; i--)
        {
            Node node = sortedTargetNodes[i];
            if (node.DistanceToNode(transform.position) - closestDistance <= 5)
                continue;
            else
                sortedTargetNodes.Remove(node);
        }
    }

    private List<Node> SortClosestNode(List<Node> enemyNodes)
    {
        List<Node> closestNodes = enemyNodes.OrderBy(o => o.DistanceToNode(transform.position)).ToList();
        return closestNodes;
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
}