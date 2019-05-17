using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Gold { get; private set; }
    public bool HasChanged { get; set; }

    public List<GameObject> benchHeroes { get; private set; }
    public List<GameObject> boardHeroes { get; private set; }

    public int amountOfBenchHeroes { get; private set; }
    public int amountOfBoardHeroes { get; private set; }

    private BenchManager benchManager;

    public Inventory(List<GameObject> benchHeroes, List<GameObject> boardHeroes, BenchManager benchManager)
    {
        this.benchHeroes = benchHeroes;
        this.boardHeroes = boardHeroes;
        this.benchManager = benchManager;
    }

    public void AddHero(GameObject hero)
    {
        hero.transform.position = benchManager.benchTiles[amountOfBenchHeroes].spawnPosition;
        benchHeroes.Add(hero);

        amountOfBenchHeroes++;
        HasChanged = true;
    }

    public bool PlaceHeroOnBoard(GameObject hero)
    {
        bool removedSuccesfully;
        removedSuccesfully = benchHeroes.Remove(hero);

        if (!removedSuccesfully)
            return false;
        else
            boardHeroes.Add(hero);

        amountOfBenchHeroes--;
        HasChanged = true;
        return true;
    }

    public bool RetrieveHeroToBench(GameObject hero)
    {
        bool removedSuccesfully;
        removedSuccesfully = boardHeroes.Remove(hero);

        if (!removedSuccesfully)
            return false;
        else
            benchHeroes.Add(hero);

        amountOfBenchHeroes++;
        HasChanged = true;
        return true;
    }

    public bool RemoveHero(GameObject hero)
    {
        Hero heroProperties = hero.GetComponent<HeroProperties>().Hero;
        HasChanged = true;
        if (heroProperties.OnBoard)
            return boardHeroes.Remove(hero);
        else
        {
            amountOfBenchHeroes--;
            return benchHeroes.Remove(hero);
        }
            
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
