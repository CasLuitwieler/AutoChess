using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    [SerializeField]
    public Hero[] HeroRotation { get; set; }

    private Inventory inventory;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public bool BuyHero(Hero hero)
    {
        if (inventory.Gold < hero.Price)
            return false;

        inventory.SubtractGold(hero.Price);
        inventory.AddHero(hero);
        return true;
    }

    public bool SellHero(Hero hero)
    {
        bool removedSuccesfully = inventory.RemoveHero(hero);

        if (removedSuccesfully)
        {
            inventory.AddGold(hero.Price);
            return true;
        }
        else
        {
            Debug.LogError("Shop couldn't remove hero from inventory");
            inventory.SubtractGold(hero.Price);
            return false;
        }
    }
}
