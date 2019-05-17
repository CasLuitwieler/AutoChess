using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    [SerializeField]
    public GameObject[] HeroRotation { get; set; }

    private Inventory inventory;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public bool BuyHero(GameObject hero)
    {
        Hero heroProperties = hero.GetComponent<HeroProperties>().Hero;
        if (inventory.Gold < heroProperties.Price)
            return false;

        inventory.SubtractGold(heroProperties.Price);
        inventory.AddHero(hero);
        return true;
    }

    public bool SellHero(GameObject hero)
    {
        Hero heroProperties = hero.GetComponent<HeroProperties>().Hero;

        bool removedSuccesfully = inventory.RemoveHero(hero);

        if (removedSuccesfully)
        {
            inventory.AddGold(heroProperties.Price);
            return true;
        }
        else
        {
            Debug.LogError("Shop couldn't remove hero from inventory");
            inventory.SubtractGold(heroProperties.Price);
            return false;
        }
    }
}
