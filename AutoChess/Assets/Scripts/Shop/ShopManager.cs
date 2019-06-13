using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Shop shop { get; private set; }

    public bool HasChanged { get; set; }

    [SerializeField]
    private GameObject heroPrefab = null;

    [SerializeField]
    private Hero[] availableHeroes = null;

    private Player player;

    private int rerollPrice = 2;

    private void Awake()
    {
        shop = new Shop();
        player = FindObjectOfType<Player>();

        shop.SetInventory(player.Inventory);
        shop.HeroRotation = new GameObject[5];
    }

    private void Start()
    {
        SetRandomHeroes();
    }

    public void SetRandomHeroes()
    {
        int amountOfHeroes = availableHeroes.Length;

        for(int i = 0; i < shop.HeroRotation.Length; i++)
        {
            GameObject newHero = Instantiate(heroPrefab, new Vector3(-100f, 0f, -100f), Quaternion.identity);

            HeroProperties heroProperties = newHero.GetComponent<HeroProperties>();
            Hero randomHero = availableHeroes[Random.Range(0, amountOfHeroes - 1)];
            heroProperties.SetupHero(randomHero);

            shop.HeroRotation[i] = newHero;
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
