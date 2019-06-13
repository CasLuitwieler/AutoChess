using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    public bool IsMovingHero { get; private set; }

    [SerializeField]
    private BoardManager boardManager = null;

    private GameObject heroBeingMoved;

    public void SetMovingHero(GameObject hero)
    {
        IsMovingHero = true;
        heroBeingMoved = hero;
    }

    public void DropHeroDown(Tile targetTile)
    {
        boardManager.MoveHero(heroBeingMoved, targetTile);

        IsMovingHero = false;
    }
}
