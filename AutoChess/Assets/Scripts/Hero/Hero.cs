using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Hero : ScriptableObject
{
    public string Name = "New Hero";
    public HeroShape Shape = HeroShape.None;
    public HeroColor Color = HeroColor.None;
    public int Price = 0;
    public int currentTile = -1;

    public bool OnBoard { get; set; }
    public bool OnBench { get; set; }
}

public enum HeroShape
{
    Cube,
    Spere,
    Capsule,
    Cylinder,
    None
}

public enum HeroColor
{
    Red,
    Green,
    Blue,
    Magenta,
    Yellow,
    Cyan,
    Gray,
    Grey,
    None
}
