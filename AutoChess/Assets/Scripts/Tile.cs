using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int TileNumber { get; set; }
    public TileState TileState { get; set; }
    public bool isBenchTile;
    
    public Vector3 spawnPosition { get; private set; }

    [SerializeField]
    private Transform spawnPoint = null;

    [SerializeField]
    private UnitManager unitManger = null;
    
    private void Awake()
    {
        spawnPosition = spawnPoint.position;
        unitManger = FindObjectOfType<UnitManager>();
    }

    private void OnMouseDown()
    {
        if(unitManger.IsMovingHero)
            unitManger.DropHeroDown(this);
    }
}

public enum TileState
{
    Available,
    Targetted,
    Occupied,
}
