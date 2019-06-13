using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private GameObject heroPrefab = null, heroContainer = null;

    public GameObject CreateHero(Vector3 spawnPosition)
    {
        return Instantiate(heroPrefab, spawnPosition, Quaternion.identity, heroContainer.transform);
    }
}
