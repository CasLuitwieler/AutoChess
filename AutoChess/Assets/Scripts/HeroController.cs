using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public bool OnBoard { get; set; }
    public int CurrentTile { get; private set; }
    public int TargetMoveTile { get; private set; }

    private UnitManager unitManager;
    private BoardManager boardManager;

    private void Awake()
    {
        unitManager = FindObjectOfType<UnitManager>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    public void OnTile(int tile)
    {
        CurrentTile = tile;
    }

    public void SetTargetMoveTile(int tile)
    {
        TargetMoveTile = tile;
    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!unitManager.IsMovingHero)
                unitManager.SetMovingHero(this.gameObject);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            BoardManager newBoardManager = FindObjectOfType<BoardManager>();
            if (OnBoard)
                boardManager.PlaceOnBench(this.gameObject);
        }
    }
}
