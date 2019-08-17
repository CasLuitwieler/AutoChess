using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI
{
    private LineRenderer line;
    private Vector3 targetPoint;
    private List<Vector3> targetPoints = new List<Vector3>();
    private int currentPoint;

    public DebugUI(LineRenderer line)
    {
        this.line = line;
    }

    public void AddPoint(Vector3 point)
    {
        targetPoints.Add(point);
    }

    private void DrawLine(Vector3 targetPosition)
    {
        currentPoint++;
        line.SetPosition(currentPoint, line.transform.position);
        currentPoint++;
        line.SetPosition(currentPoint, targetPosition);
    }

    public void SetActive(bool isSelected)
    {
        line.enabled = isSelected;
    }

    public void UpdateLines()
    {
        line.positionCount = targetPoints.Count * 2;
        line.widthMultiplier = 0.2f;

        currentPoint = -1;
        for (int i = 0; i < targetPoints.Count; i++)
        {
            DrawLine(targetPoints[i]);
        }
    }
}