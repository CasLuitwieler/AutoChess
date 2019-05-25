using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isBenchTile;

    [SerializeField]
    private Transform spawn = null;

    [SerializeField]
    private UnitManager unitManger = null;

    public Vector3 spawnPosition { get; private set; }

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
