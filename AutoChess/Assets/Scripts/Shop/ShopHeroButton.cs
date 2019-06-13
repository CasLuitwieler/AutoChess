using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHeroButton : MonoBehaviour
{
    [SerializeField]
    private Text heroName = null, heroPrice = null;
    [SerializeField]
    private Image heroIcon = null;

    [SerializeField]
    private Sprite square = null, circle = null;

    //private Hero hero = null;
    
    public void SetHero(Hero hero)
    {
        //this.hero = hero;
        
        SetShape(hero.Shape);
        SetColor(hero.Color);

        //set hero name text
        heroName.text = hero.Name;
        //set hero price text
        heroPrice.text = hero.Price.ToString();
    }
     
    private void SetShape(HeroShape shape)
    {
        switch (shape)
        {
            case HeroShape.Cube:
                heroIcon.sprite = square;
                break;
            case HeroShape.Spere:
                heroIcon.sprite = circle;
                break;
            default:
                heroIcon.sprite = square;
                Debug.LogError("Hero in shop has unexpected shape");
                break;
        }
    }

    private void SetColor(HeroColor color)
    {
        switch (color)
        {
            case HeroColor.Red:
                heroIcon.color = Color.red;
                break;
            case HeroColor.Green:
                heroIcon.color = Color.green;
                break;
            case HeroColor.Blue:
                heroIcon.color = Color.blue;
                break;
            case HeroColor.Magenta:
                heroIcon.color = Color.magenta;
                break;
            case HeroColor.Yellow:
                heroIcon.color = Color.yellow;
                break;
            case HeroColor.Cyan:
                heroIcon.color = Color.cyan;
                break;
            case HeroColor.Gray:
                heroIcon.color = Color.gray;
                break;
            case HeroColor.Grey:
                heroIcon.color = Color.grey;
                break;
            default:
                heroIcon.color = Color.black;
                Debug.LogError("Hero in shop has unexpected color");
                break;
        }
    }
}
