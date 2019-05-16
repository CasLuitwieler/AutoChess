using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Gold { get; private set; }
    public bool HasChanged { get; set; }

    public List<Hero> benchHeroes { get; private set; }
    public List<Hero> boardHeroes { get; private set; }
    
    public Inventory(List<Hero> benchHeroes, List<Hero> boardHeroes)
    {
        this.benchHeroes = benchHeroes;
        this.boardHeroes = boardHeroes;
    }

    public void AddHero(Hero hero)
    {
        benchHeroes.Add(hero);
        HasChanged = true;
    }

    public bool PlaceHeroOnBoard(Hero hero)
    {
        bool removedSuccesfully;
        removedSuccesfully = benchHeroes.Remove(hero);

        if (!removedSuccesfully)
            return false;
        else
            boardHeroes.Add(hero);
        HasChanged = true;
        return true;
    }

    public bool RetrieveHeroToBench(Hero hero)
    {
        bool removedSuccesfully;
        removedSuccesfully = boardHeroes.Remove(hero);

        if (!removedSuccesfully)
            return false;
        else
            benchHeroes.Add(hero);
        HasChanged = true;
        return true;
    }

    public bool RemoveHero(Hero hero)
    {
        HasChanged = true;
        if (hero.OnBoard)
            return boardHeroes.Remove(hero);
        else
            return benchHeroes.Remove(hero);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public void SubtractGold(int amount)
    {
        Gold -= amount;
    }
}
