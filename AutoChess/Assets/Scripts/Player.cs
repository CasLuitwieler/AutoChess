using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory { get; private set; }
    
    public int Level { get; private set; }

    [SerializeField]
    private BenchManager benchManager = null;

    private void Awake()
    {
        Inventory = new Inventory(new List<GameObject>(), new List<GameObject>(), benchManager);
    }
}

public class LevelManger
{
    public int Level { get; set; }

    public int Experience { get; set; }
}