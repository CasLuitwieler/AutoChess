using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour
{
    public Tile[] BenchTiles = new Tile[8];
    public Tile[] BoardTiles = new Tile[64];
    
    private List<GameObject> heroes = new List<GameObject>();

    private GameObject[] benchHeroes = new GameObject[8];
    private GameObject[] boardHeroes = new GameObject[10];

    private GameObject[] currentArray, targetArray;
    
    public void PlaceOnBench(GameObject hero)
    {
        HeroController heroController = hero.GetComponent<HeroController>();

        //remove from board if the hero was on the board
        if(heroController.OnBoard)
        {
            if (CheckForEntry(boardHeroes, out int oldEntry, hero))
                boardHeroes[oldEntry] = null;
        }

        //add the hero to the bench at the first empty entry
        if (CheckForEntry(benchHeroes, out int targetIndex, null))
            benchHeroes[targetIndex] = hero;

        //set the hero position to the tile it has moved to
        if(BenchTiles.Length > 0)
            hero.transform.position = BenchTiles[targetIndex].spawnPosition;

        //store current tile id
        heroController.OnTile(targetIndex);
    }

    public bool AvailableSpotOnBench()
    {
        return CheckForEntry(benchHeroes, out int temp, null);
    }

    public void MoveHero(GameObject heroToMove, Tile targetTile)
    {
        Move moveType = CalculateMoveType(heroToMove, targetTile);
        SetCurrentAndTargetArray(moveType);
        
        if(ValidSwap(heroToMove, out int oldIndex, out int newIndex))
            SwapHero(heroToMove, oldIndex, newIndex);

        PlaceHero(heroToMove, targetTile);
    }

    private bool ValidSwap(GameObject heroToMove, out int oldIndex, out int newIndex)
    {
        oldIndex = newIndex = -1;
        //find the heroToMove in the board array
        if (CheckForEntry(currentArray, out oldIndex, heroToMove))
        {
            //find the heroToMove in the board array
            if (CheckForEntry(currentArray, out newIndex, heroToMove))
            {
                return true;
            }
        }
        return false;
    }
    
    private void SwapHero(GameObject heroToMove, int oldIndex, int newIndex)
    {
        //remove the hero from the board
        currentArray[oldIndex] = null;
        //add the hero to the bench
        targetArray[newIndex] = heroToMove;
    }

    private void PlaceHero(GameObject heroToMove, Tile targetTile)
    {
        int targetTileIndex;

        HeroController heroController = heroToMove.GetComponent<HeroController>();
        if (targetTile.isBenchTile)
        {
            targetTileIndex = Array.IndexOf(BenchTiles, targetTile);
            heroController.OnBoard = false;
        }
        else
        {
            targetTileIndex = Array.IndexOf(BoardTiles, targetTile);
            heroController.OnBoard = true;
        }

        //store current tile id
        heroController.OnTile(targetTileIndex);
        //move player to targetTile position
        heroToMove.transform.position = targetTile.spawnPosition;
    }

    private Move CalculateMoveType(GameObject hero, Tile targetTile)
    {
        HeroController heroController = hero.GetComponent<HeroController>();

        if (heroController.OnBoard && !targetTile.isBenchTile)
            return Move.BoardToBoard;
        else if (!heroController.OnBoard && targetTile.isBenchTile)
            return Move.BenchToBench;
        else if (heroController.OnBoard && targetTile.isBenchTile)
            return Move.BoardToBench;
        else if (!heroController.OnBoard && !targetTile.isBenchTile)
            return Move.BenchToBoard;
        else
            return Move.None;
    }

    private void SetCurrentAndTargetArray(Move moveType)
    {
        switch (moveType)
        {
            case Move.BenchToBench:
                currentArray = benchHeroes;
                targetArray = benchHeroes;
                break;
            case Move.BenchToBoard:
                currentArray = benchHeroes;
                targetArray = boardHeroes;
                break;
            case Move.BoardToBench:
                currentArray = boardHeroes;
                targetArray = benchHeroes;
                break;
            case Move.BoardToBoard:
                currentArray = boardHeroes;
                targetArray = boardHeroes;
                break;
            default:
                Debug.LogError("Invalid hero moveType");
                break;
        }
    }

    private bool CheckForEntry(GameObject[] heroArray, out int index, GameObject heroToFind)
    {
        for (int i = 0; i < heroArray.Length; i++)
        {
            if (heroArray[i] == heroToFind)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

    public GameObject[] GetBoardHeroes()
    {
        return boardHeroes;
    }
}

public enum Move
{
    BenchToBench,
    BenchToBoard,
    BoardToBench,
    BoardToBoard,
    None
}