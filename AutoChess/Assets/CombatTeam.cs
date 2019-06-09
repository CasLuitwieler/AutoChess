using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTeam
{
    public List<NewHero> Heroes { get; private set; }

    private Team Team;
    private List<int> HeroTiles;
    private Tile[] boardTiles;
    private HeroManager heroManager;
    private List<StateMachine> heroStateMachines;

    public CombatTeam(List<int> heroTiles, Team team, Tile[] boardTiles, HeroManager heroManager)
    {
        Team = team;
        HeroTiles = heroTiles;
        this.boardTiles = boardTiles;
        this.heroManager = heroManager;

        Heroes = new List<NewHero>();
        heroStateMachines = new List<StateMachine>();

        CreateHeroes();
    }

    private void CreateHeroes()
    {
        for (int i = 0; i < HeroTiles.Count; i++)
        {
            //int tileNumber = HeroTiles[i];
            int tileNumber;
            if (Team == Team.Player)
                tileNumber = 3;
            else
                tileNumber = Random.Range(32, 63);
            HeroTiles[i] = tileNumber;

            Vector3 spawnPosition = boardTiles[tileNumber].spawnPosition;
            GameObject hero = heroManager.CreateHero(spawnPosition);
            NewHero newHero = hero.GetComponent<NewHero>();
            Heroes.Add(newHero);

            Heroes[i].SetCurrentTile(tileNumber);
            Heroes[i].Team = Team;
            Heroes[i].SetTiles(currentTile: tileNumber, boardTiles);

            heroStateMachines.Add(Heroes[i].GetComponent<StateMachine>());
            heroStateMachines[i].CreateCylceManager(newHero.CycleTime, newHero.PauseModifier);
        }
    }

    public void Update()
    {
        foreach (StateMachine hero in heroStateMachines)
            hero.UpdateCycle();
    }

    public void SetTargetHeroes(List<NewHero> targetHeroes)
    {
        foreach (NewHero hero in Heroes)
            hero.SetTargetHeroes(targetHeroes);
    }
}
