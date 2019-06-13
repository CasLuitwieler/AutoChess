using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProperties : MonoBehaviour
{
    public Hero Hero { get; private set; }

    [SerializeField]
    private Mesh cube = null, spere = null, capsule = null, cylinder = null;

    public void SetupHero(Hero hero)
    {
        this.Hero = hero;
        SetShape();
        SetColor();
    }
    
    private void SetShape()
    {
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        switch (Hero.Shape)
        {
            case HeroShape.Cube:
                meshFilter.mesh = cube;
                break;
            case HeroShape.Spere:
                meshFilter.mesh = spere;
                break;
            case HeroShape.Capsule:
                meshFilter.mesh = capsule;
                break;
            case HeroShape.Cylinder:
                meshFilter.mesh = cylinder;
                break;
        }
    }

    private void SetColor()
    {
        Renderer rend = GetComponentInChildren<Renderer>();

        switch(Hero.Color)
        {
            case HeroColor.Red:
                rend.material.color = Color.red;
                break;
            case HeroColor.Green:
                rend.material.color = Color.green;
                break;
            case HeroColor.Blue:
                rend.material.color = Color.blue;
                break;
            case HeroColor.Magenta:
                rend.material.color = Color.magenta;
                break;
            case HeroColor.Yellow:
                rend.material.color = Color.yellow;
                break;
            case HeroColor.Cyan:
                rend.material.color = Color.cyan;
                break;
            case HeroColor.Gray:
                rend.material.color = Color.gray;
                break;
            case HeroColor.Grey:
                rend.material.color = Color.grey;
                break;
            default:
                rend.material.color = Color.black;
                break;
        }
    }
    
}
