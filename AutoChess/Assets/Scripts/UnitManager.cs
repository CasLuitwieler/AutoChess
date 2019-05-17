using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    public bool IsMovingHero { get; private set; }

    [SerializeField]
    private Image movingHeroImage;

    private GameObject hero;

    public void MovingHero(GameObject hero)
    {
        IsMovingHero = true;
        this.hero = hero;
    }

    public void DropHeroDown(GameObject targetTile)
    {
        Vector3 newPosition = targetTile.GetComponent<Tile>().spawnPosition;
        hero.transform.position = newPosition;
        IsMovingHero = false;
    }
}
