using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SimulationHero
{
    public Transform transform { get; private set; }

    public Node targetNode = null;
    private Vector3 targetMovePos;

    private DebugUI debugLine;
    
    public SimulationHero(Transform transform)
    {
        this.transform = transform;
        debugLine = new DebugUI(transform.GetComponent<LineRenderer>());
    }

    public void CalculateMove(List<Node> enemyNodes)
    {
        FindTargetTile(enemyNodes);

        debugLine.UpdateLines();
    }

    public void SetLineActive(bool isSelected)
    {
        debugLine.SetActive(isSelected);
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

        targetNode = sortedTargetNodes[0];

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