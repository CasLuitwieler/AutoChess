using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isBenchTile;
    public bool isOccupied { get; set; }
    public bool isTargetted { get; set; }

    public Vector3 spawnPosition { get; private set; }

    [SerializeField]
    private Transform spawn = null;

    [SerializeField]
    private UnitManager unitManger = null;
    
    private void Awake()
    {
        spawnPosition = spawn.position;
        unitManger = FindObjectOfType<UnitManager>();
    }

    private void OnMouseDown()
    {
        if(unitManger.IsMovingHero)
            unitManger.DropHeroDown(this);
    }
}
