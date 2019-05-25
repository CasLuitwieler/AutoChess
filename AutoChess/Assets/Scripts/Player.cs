using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Inventory Inventory { get; private set; }
    
    public int Level { get; private set; }

    [SerializeField]
    private Text goldText = null;

    private void Awake()
    {
        Inventory = new Inventory(new List<GameObject>(), new List<GameObject>());
        Inventory.AddGold(50);
    }

    private void Update()
    {
        goldText.text = "Gold: " + Inventory.Gold.ToString();
    }
}

public class LevelManger
{
    public int Level { get; set; }

    public int Experience { get; set; }
}