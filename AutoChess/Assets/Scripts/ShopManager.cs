using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Hero[] availableHeroes;

    public Shop shop { get; private set; }

    public bool HasChanged { get; set; }

    private Player player;

    private int rerollPrice = 2;

    private void Awake()
    {
        shop = new Shop();
        player = FindObjectOfType<Player>();

        shop.SetInventory(player.Inventory);
        shop.HeroRotation = new Hero[5];
    }

    private void Start()
    {
        SetRandomHeroes();
    }

    public void SetRandomHeroes()
    {
        int amountOfHeroes = shop.HeroRotation.Length;

        for(int i = 0; i < amountOfHeroes; i++)
        {
            shop.HeroRotation[i] = availableHeroes[Random.Range(0, amountOfHeroes - 1)];
        }
        HasChanged = true;
    }

    public void Reroll()
    {
        if (player.Inventory.Gold < rerollPrice)
            return;
        
        player.Inventory.SubtractGold(rerollPrice);
        SetRandomHeroes();
    }
}
