using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform spawn;

    public Vector3 spawnPosition { get; private set; }

    public bool tileOccupied { get; set; }

    private void Awake()
    {
        spawnPosition = spawn.position;
    }
}
