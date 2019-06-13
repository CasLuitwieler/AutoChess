using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHelper : MonoBehaviour
{
    private HeroMover gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<HeroMover>();
    }

    private void OnMouseDown()
    {
        gameManager.PlaceHero();
    }
}
