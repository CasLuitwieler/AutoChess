using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager = null;

    [SerializeField]
    private GameObject heroPrefab = null;

    private GameObject[] playerHeroes;
    private GameObject[] enemyHeroes;

    private List<int> playerHeroTiles = new List<int>();
    private List<int> enemyHeroTiles = new List<int>();

    [SerializeField]
    private int amountOfEnemies = 2;

    private List<HeroController> playerHeroControllers = new List<HeroController>();
    private List<HeroController> enemyHeroControllers = new List<HeroController>();
    
    public void SetupCombat()
    {
        CreateEnemies();

        GetHeroControllers();

        SetCurrentTile();

        roundTimeCounter = 0;
        CalculateHeroMoves();
    }
    
    private void GetHeroControllers()
    {
        playerHeroes = boardManager.GetBoardHeroes();

        foreach (GameObject hero in playerHeroes)
        {
            if (!hero) continue;
            playerHeroControllers.Add(hero.GetComponent<HeroController>());
        }
    }

    private void SetCurrentTile()
    {
        for (int i = 0; i < playerHeroControllers.Count; i++)
        {
            playerHeroTiles.Add(playerHeroControllers[i].CurrentTile);
        }
    }

    private void CreateEnemies()
    {
        enemyHeroes = new GameObject[amountOfEnemies];
        for (int i = 0; i < enemyHeroes.Length; i++)
        {
            int tileNumber = Random.Range(32, 63);
            Vector3 spawnPos = boardManager.BoardTiles[tileNumber].spawnPosition;
            enemyHeroes[i] = Instantiate(heroPrefab, spawnPos, Quaternion.identity);
            enemyHeroes[i].GetComponent<HeroController>().OnTile(tileNumber);
            enemyHeroTiles.Add(tileNumber);
        }
    }

    private float roundTime = 5f;
    private float roundTimeCounter;
    private bool updatedTiles = false;

    private void Update()
    {
        roundTimeCounter += Time.deltaTime;
        if(roundTimeCounter <= roundTime / 2)
        {
            MoveHeroes();
        }
        else if(roundTimeCounter > roundTime / 2 && !updatedTiles)
        {
            UpdateTiles();
            updatedTiles = true;
        }
        if(roundTimeCounter >= roundTime)
        {
            roundTimeCounter = 0;
            updatedTiles = false;
            CalculateHeroMoves();
        }
    }

    private void CalculateHeroMoves()
    {
        foreach (HeroController playerHero in playerHeroControllers)
        {
            CalculateMove(playerHero, enemyHeroTiles);
        }
    }

    private void UpdateTiles()
    {
        foreach(HeroController playerHero in playerHeroControllers)
        {
            playerHero.OnTile(playerHero.TargetMoveTile);
        }
    }

    private void MoveHeroes()
    {
        foreach (HeroController playerHero in playerHeroControllers)
        {
            if (playerHero.CurrentTile != playerHero.TargetMoveTile)
                playerHero.transform.position = Vector3.Lerp(
                    playerHero.transform.position,
                    boardManager.BoardTiles[playerHero.TargetMoveTile].spawnPosition,
                    roundTimeCounter / 2.5f);
        }
    }

    private void CalculateMove(HeroController heroController, List<int> targetTiles)
    {
        int currentTile = heroController.CurrentTile;
        List<int> remainingTargets = targetTiles;
        int targetTile = -1;
        int xDifference = -1, yDifference = -1;

        while(remainingTargets.Count > 0)
        {
            int targetHeroTile = GetClosestHeroTile(currentTile, remainingTargets, out int xDif, out int yDif);
            if (CheckTileAvailable(targetHeroTile, xDif, yDif, out int availableTile))
            {
                targetTile = availableTile;
                xDifference = xDif;
                yDifference = yDif;
                break;
            }
            else
                remainingTargets.Remove(targetHeroTile);
        }
        int targetMoveTile = -1;
        int xDifferenceAbs = Mathf.Abs(xDifference);
        int yDifferenceAbs = Mathf.Abs(yDifference);

        int difference = Mathf.Abs(xDifferenceAbs - yDifferenceAbs);

        if (xDifferenceAbs > yDifferenceAbs)
        {
            if (xDifference > 0)
                targetMoveTile = currentTile + Mathf.Clamp(difference, 0, 3);
            else
                targetMoveTile = currentTile - Mathf.Clamp(difference, 0, 3);
        }

        if(xDifferenceAbs < yDifferenceAbs)
        {
            if (yDifference > 0)
                targetMoveTile = currentTile + (Mathf.Clamp(difference, 0, 3) * 8);
            else if (yDifference < 0)
                targetMoveTile = currentTile - (Mathf.Clamp(difference, 0, 3) * 8);
        }

        if(xDifferenceAbs == yDifferenceAbs)
        {
            targetMoveTile = currentTile + Mathf.Clamp(xDifference, -2, 2) + (Mathf.Clamp(yDifference, -2, 2) * 8);

            if(xDifferenceAbs == 0)
            {
                boardManager.BoardTiles[currentTile].isOccupied = true;
                heroController.SetTargetMoveTile(currentTile);
                return;
            }

            /*
            if (xDifference > 0 && yDifference > 0)
                targetMoveTile = currentTile + xDifference + (yDifference * 8);
            else if (xDifference < 0 && yDifference < 0)
                targetMoveTile = currentTile - Mathf.Abs(xDifference) - Mathf.Abs(yDifference * 8);
            else if(xDifference > 0 && yDifference < 0)
                targetMoveTile = currentTile + xDifference - Mathf.Abs((yDifference * 8));
            else if(xDifference < 0 && yDifference > 0)
                targetMoveTile = currentTile + 
            else
            {
                boardManager.BoardTiles[currentTile].isOccupied = true;
                heroController.SetTargetMoveTile(currentTile);
                return;
            }
            */
        }
        boardManager.BoardTiles[targetMoveTile].isTargetted = true;
        heroController.SetTargetMoveTile(targetMoveTile);

        Debug.Log("targetMoveTile: " + targetMoveTile);
    }

    private bool CheckTileAvailable(int targetHeroTile, int xDif, int yDif, out int availableTile)
    {
        (int horizontalTargetTile, int verticalTargetTile) = GetSurroundingTiles(targetHeroTile, xDif, yDif, out int horizontalDirection, out int verticalDirection);

        if (GetAvailableTile(horizontalTargetTile, verticalTargetTile, horizontalDirection, verticalDirection, ref xDif, ref yDif, out availableTile))
            return true;
        return false;
    }

    private int GetClosestHeroTile(int currentTile, List<int> targetTiles, out int xDif, out int yDif)
    {
        int targetTile = -1;
        xDif = yDif = 0;
        float squaredDistance, closestDistance = float.MaxValue;

        foreach(int tile in targetTiles)
        {
            squaredDistance = CalculateSquaredTileDistance(currentTile, tile, out int xDifference, out int yDifference);
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

    public (int, int) GetSurroundingTiles(int targetHeroTile, int xDif, int yDif, out int horizontalDirection ,out int verticalDirection)
    {
        int horizontalTargetTile = -1;
        int verticalTargetTile = -1;

        if (xDif > 0)
        {
            horizontalTargetTile = targetHeroTile - 1;
            horizontalDirection = -1;
        }
        else
        {
            horizontalTargetTile = targetHeroTile + 1;
            horizontalDirection = 1;
        }
        if (yDif > 0)
        {
            verticalTargetTile = targetHeroTile - 8;
            verticalDirection = -1;
        }
        else
        {
            verticalTargetTile = targetHeroTile + 8;
            verticalDirection = 1;
        }
        
        return (horizontalTargetTile, verticalTargetTile);
    }

    public bool GetAvailableTile(int horizontalTile, int verticalTile, int horizontalDirection, int verticalDirection, ref int xDif, ref int yDif, out int availableTile)
    {
        if (Mathf.Abs(xDif) - Mathf.Abs(yDif) >= 0)
        {
            //horizontal
            if(!boardManager.BoardTiles[horizontalTile].isOccupied && !boardManager.BoardTiles[horizontalTile].isTargetted)
            {
                availableTile = horizontalTile;
                xDif += horizontalDirection;
                return true;
            }
        }
        //vertical
        if (!boardManager.BoardTiles[verticalTile].isOccupied && !boardManager.BoardTiles[verticalTile].isTargetted)
        {
            availableTile = verticalTile;
            yDif += verticalDirection;
            return true;
        }
        availableTile = -1;
        return false;
    }

    private float CalculateSquaredTileDistance(int currentTile, int targetTile, out int xDif, out int yDif)
    {
        xDif = (targetTile % 8) - (currentTile % 8);
        yDif = Mathf.RoundToInt(targetTile / 8) - Mathf.RoundToInt(currentTile / 8);
        return (xDif * xDif) + (yDif * yDif);
    }
}
