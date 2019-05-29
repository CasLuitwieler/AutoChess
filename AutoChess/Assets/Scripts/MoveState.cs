using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private BoardManager boardManager;
    private HeroController heroController;

    int xDif, yDif;
    int xDifAbs, yDifAbs;
    int distance, distanceSquared;
    int currentTile, targetTile;

    public MoveState(NewHero hero) : base(hero)
    {
        
    }

    public override Type Tick()
    {
        throw new NotImplementedException();
    }

    private void CalculateMove(List<int> targetTiles)
    {
        currentTile = heroController.CurrentTile;
        List<int> remainingTargets = targetTiles;

        while (remainingTargets.Count > 0)
        {
            int targetHeroTile = GetClosestHeroTile(remainingTargets);
            if (GetClosestAvailableTile(targetHeroTile, out int availableTile))
            {
                SetNewTarget(availableTile);
                break;
            }
            else
                remainingTargets.Remove(targetHeroTile);
        }
        int targetMoveTile = -1;
        xDifAbs = Mathf.Abs(xDif);
        yDifAbs = Mathf.Abs(yDif);

        int difference = Mathf.Abs(xDifAbs - yDifAbs);

        if (xDifAbs > yDifAbs)
        {
            if (xDif > 0)
                targetMoveTile = currentTile + Mathf.Clamp(difference, 0, 3);
            else
                targetMoveTile = currentTile - Mathf.Clamp(difference, 0, 3);
        }

        if (xDifAbs < yDifAbs)
        {
            if (yDif > 0)
                targetMoveTile = currentTile + (Mathf.Clamp(difference, 0, 3) * 8);
            else if (yDif < 0)
                targetMoveTile = currentTile - (Mathf.Clamp(difference, 0, 3) * 8);
        }

        if (xDifAbs == yDifAbs)
        {
            targetMoveTile = currentTile + Mathf.Clamp(xDif, -2, 2) + (Mathf.Clamp(yDif, -2, 2) * 8);

            if (xDifAbs == 0)
            {
                boardManager.BoardTiles[currentTile].isOccupied = true;
                heroController.SetTargetMoveTile(currentTile);
                return;
            }
        }
        boardManager.BoardTiles[targetMoveTile].isTargetted = true;
        heroController.SetTargetMoveTile(targetMoveTile);

        Debug.Log("targetMoveTile: " + targetMoveTile);
    }

    private int GetClosestHeroTile(List<int> targetTiles)
    {
        int targetTile = -1;
        xDif = yDif = 0;
        float squaredDistance, closestDistance = float.MaxValue;

        foreach (int tile in targetTiles)
        {
            squaredDistance = CalculateSquaredTileDistance(tile, out int xDifference, out int yDifference);
            if (squaredDistance < closestDistance)
            {
                closestDistance = squaredDistance;
                targetTile = tile;
                xDif = xDifference;
                yDif = yDifference;
            }
        }
        return targetTile;
    }

    private bool GetClosestAvailableTile(int targetHeroTile, out int closestAvailableTile)
    {
        closestAvailableTile = -1;
        float closestDistance = float.MaxValue;
        float squaredDistance;

        //gets all tiles surrounding the targetHeroTile
        GetSurroundingTiles(targetHeroTile, out List<int> surroundingTiles);

        foreach(int tile in surroundingTiles)
        {
            squaredDistance = CalculateSquaredTileDistance(tile, out int xDifference, out int yDifference);
            if (squaredDistance <= closestDistance)
                continue;

            if (TileAvailable(tile, out int availableTile))
                closestAvailableTile = availableTile;
        }

        if (closestAvailableTile >= 0)
            return true;
        return false;
    }

    public void GetSurroundingTiles(int targetHeroTile, out List<int> surroundingTiles)
    {
        surroundingTiles = new List<int>();
        surroundingTiles.Add(targetHeroTile - 1); //left tile
        surroundingTiles.Add(targetHeroTile + 7); //top left tile
        surroundingTiles.Add(targetHeroTile + 8); //top tile
        surroundingTiles.Add(targetHeroTile + 9); //top right tile
        surroundingTiles.Add(targetHeroTile + 1); //right tile
        surroundingTiles.Add(targetHeroTile - 7); //bottom right tile
        surroundingTiles.Add(targetHeroTile - 8); //bottom tile
        surroundingTiles.Add(targetHeroTile - 9); //bottom left tile

        if (targetHeroTile % 8 == 0)
        {
            //left side
            surroundingTiles.Remove(targetHeroTile +7); //top left tile
            surroundingTiles.Remove(targetHeroTile -1); //left tile
            surroundingTiles.Remove(targetHeroTile -9); //bottom left tile
        }
        if(targetHeroTile % 8 == 7)
        {
            //right side
            surroundingTiles.Remove(targetHeroTile + 9); //top right tile
            surroundingTiles.Remove(targetHeroTile + 1); //right tile
            surroundingTiles.Remove(targetHeroTile - 7); //bottom right tile
        }
        if(targetHeroTile < 7)
        {
            //bottom
            surroundingTiles.Remove(targetHeroTile - 9); //bottom left tile
            surroundingTiles.Remove(targetHeroTile - 8); //bottom tile
            surroundingTiles.Remove(targetHeroTile - 7); //bottom right tile
        }
        if(targetHeroTile > 55)
        {
            //top
            surroundingTiles.Remove(targetHeroTile + 7); //top left tile
            surroundingTiles.Remove(targetHeroTile + 8); //top tile
            surroundingTiles.Remove(targetHeroTile + 9); //top right tile
        }
    }

    public bool TileAvailable(int targetTile, out int availableTile)
    {
        availableTile = -1;
        if (!boardManager.BoardTiles[targetTile].isOccupied && !boardManager.BoardTiles[targetTile].isTargetted)
        {
            availableTile = targetTile;
            return true;
        }
        return false;
    }

    private void SetNewTarget(int targetTile)
    {
        xDif = (targetTile % 8) - (currentTile % 8);
        yDif = Mathf.RoundToInt(targetTile / 8) - Mathf.RoundToInt(currentTile / 8);
        this.targetTile = targetTile;
    }

    private float CalculateSquaredTileDistance(int targetTile, out int xDifference, out int yDifference)
    {
        xDifference = (targetTile % 8) - (currentTile % 8);
        yDifference = Mathf.RoundToInt(targetTile / 8) - Mathf.RoundToInt(currentTile / 8);
        return (xDifference * xDifference) + (yDifference * yDifference);
    }
}
