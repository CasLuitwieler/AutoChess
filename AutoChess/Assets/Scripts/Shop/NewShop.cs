using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewShop : MonoBehaviour
{
    [SerializeField]
    private Hero[] availableHeroes = null;

    [SerializeField]
    private ShopHeroButton[] shopHeroButtons = null;

    private Hero[] heroRotation = new Hero[5];

    [SerializeField]
    private BoardManager boardManager = null;

    [SerializeField]
    private GameObject heroPrefab = null;

    [SerializeField]
    private GameObject heroContainer = null;

    [SerializeField]
    private Player player = null;

    private Inventory inventory = null;

    private int rerollPrice = 2;

    private void Start()
    {
        inventory = player.Inventory;

        SetRandomHeroes();
    }

    public void SetRandomHeroes()
    {
        int amountOfHeroes = availableHeroes.Length;
        
        for (int i = 0; i < shopHeroButtons.Length; i++)
        {
            Hero randomHero = availableHeroes[Random.Range(0, amountOfHeroes - 1)];
            shopHeroButtons[i].SetHero(randomHero);
            heroRotation[i] = randomHero;
        }
    }

    public void BuyHero(int heroID)
    {
        //check if the shop hero hasn't been purchased already
        if (heroRotation[heroID] == null)
            return;
        Hero purchasedHero = heroRotation[heroID];

        //if the player doesn't have enough gold, return
        if (inventory.Gold < purchasedHero.Price)
        {
            Debug.Log("Not enought gold to purchase hero");
            return;
        }

        inventory.SubtractGold(purchasedHero.Price);

        //if the bench is full, return
        if (!boardManager.AvailableSpotOnBench())
            return;
        
        //instantiate hero object
        GameObject newHero = Instantiate(heroPrefab, heroContainer.transform);
        //set the hero properties
        HeroProperties heroProperties = newHero.GetComponent<HeroProperties>();
        heroProperties.SetupHero(purchasedHero);
        //add hero to the benchHeroes
        boardManager.PlaceOnBench(newHero);

        //make sure the same hero can't be purchased multiple timnes
        heroRotation[heroID] = null;
    }

    public void Reroll()
    {
        if (inventory.Gold < rerollPrice)
            return;

        inventory.SubtractGold(rerollPrice);

        SetRandomHeroes();
    }
}
