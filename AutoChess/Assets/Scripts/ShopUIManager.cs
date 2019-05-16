using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField]
    private ShopManager shopManager = null;
    private Shop shop;

    private Button[] buttons;
    private Text[] nameTexts;

    private void Awake()
    {
        shop = shopManager.shop;
        buttons = GetComponentsInChildren<Button>();
        nameTexts = new Text[shop.HeroRotation.Length];
        for(int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i] = buttons[i].GetComponentInChildren<Text>();
        }
    }

    private void Update()
    {
        if(shopManager.HasChanged)
        {
            for(int i = 0; i < nameTexts.Length; i++)
            {
                nameTexts[i].text = shop.HeroRotation[i].Name;
            }
            shopManager.HasChanged = false;
        }
    }

    public void BuyHero(int heroID)
    {
        shop.BuyHero(shop.HeroRotation[heroID]);
    }

    public void Reroll()
    {
        shopManager.Reroll();
    }
}
