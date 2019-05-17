using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchManager : MonoBehaviour
{
    public Tile[] benchTiles { get; private set; }

    private void Awake()
    {
        benchTiles = GetComponentsInChildren<Tile>();
    }
}
