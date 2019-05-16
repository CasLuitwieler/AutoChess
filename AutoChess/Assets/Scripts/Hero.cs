using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Hero : ScriptableObject
{
    public string Name = "New Hero";
    public HeroSpecies Species = HeroSpecies.None;
    public HeroClass Class = HeroClass.None;
    public int Price = 0;
    public Sprite Sprite;

    public bool OnBoard { get; set; }
}

public enum HeroSpecies
{
    Beast,
    Demon,
    Dragon,
    Dwarf,
    Elemental,
    Elf,
    Goblin,
    Satyr,
    Human,
    Ogre,
    Orc,
    Nage,
    Troll,
    Undead,
    God,
    None
}

public enum HeroClass
{
    Assassin,
    DemonHunter,
    Druid,
    Knight,
    Hunter,
    Priest,
    Mage,
    Mech,
    Shaman,
    Warlock,
    Warrior,
    None
}