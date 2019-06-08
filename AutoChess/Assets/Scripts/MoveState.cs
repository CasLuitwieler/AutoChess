﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private BoardManager boardManager;
    private NewHero hero;

    int xDif, yDif;
    int xDifAbs, yDifAbs;
    int distance, distanceSquared;
    int currentTile, targetTile, targetMoveTile;

    float moveSpeed = 0.2f;

    public MoveState(NewHero hero, BoardManager boardManager) : base(hero)
    {
        currentTile = hero.CurrentTile;
        this.hero = hero;
        this.boardManager = boardManager;
    }

    public override void CycleStart()
    {
        CalculateMove();
    }

    public override Type Tick()
    {
        //lerp to target position
        transform.position = Vector3.Lerp(transform.position, hero.TargetTiles[targetMoveTile].spawnPosition, Time.time * moveSpeed);

        if (targetTile == currentTile)
            return typeof(AttackState);
        return null;
    }

    public override void CycleEnd()
    {
        //update currentTile
        currentTile = targetMoveTile;
        hero.SetCurrentTile(currentTile);
        //reset tile state
        //boardManager.BoardTiles[targetMoveTile].TileState = TileState.Targetted;
        //reset targetMoveTile
        hero.TargetMoveTile = -1;
    }
    
    protected void CalculateMove()
    {
        if (!FindTargetTile())
        {
            targetTile = currentTile;
            return;
        }

        xDifAbs = Mathf.Abs(xDif);
        yDifAbs = Mathf.Abs(yDif);
        int difference = Mathf.Abs(xDifAbs - yDifAbs);

        Debug.Log("xDif: " + xDif + "\t yDif: " + yDif + "\t difference: " + difference);

        if (xDifAbs > yDifAbs)
        {
            targetMoveTile = currentTile + Mathf.Clamp(xDif, -3, 3);
        }
        else if (xDifAbs < yDifAbs)
        {
            targetMoveTile = currentTile + (Mathf.Clamp(yDif, -3, 3) * 8);
        }
        else if (xDifAbs == yDifAbs)
        {
            targetMoveTile = currentTile + Mathf.Clamp(xDif, -2, 2) + (Mathf.Clamp(yDif, -2, 2) * 8);

            if (xDifAbs == 0)
            {
                Debug.Log("already on target tile, not moving");
                boardManager.BoardTiles[currentTile].TileState = TileState.Occupied;
                //heroController.SetTargetMoveTile(currentTile);
                return;
            }
        }
        boardManager.BoardTiles[targetMoveTile].TileState = TileState.Targetted;
        hero.TargetMoveTile = targetMoveTile;

        Debug.Log("targetMoveTile: " + targetMoveTile);
    }

    private bool FindTargetTile()
    {
        //currentTile = heroController.CurrentTile;
        List<int> remainingTargets = new List<int>();
        foreach(Tile tile in hero.TargetTiles)
        {
            remainingTargets.Add(tile.TileNumber);
        }

        while (remainingTargets.Count > 0)
        {
            int targetHeroTile = GetClosestHeroTile(remainingTargets);
            if (GetClosestAvailableTile(targetHeroTile, out int availableTile))
            {
                SetNewTarget(availableTile);
                return true;
            }
            else
                remainingTargets.Remove(targetHeroTile);
        }
        return false;
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
