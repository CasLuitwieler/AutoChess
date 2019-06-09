using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Range(0, 31)]
    [SerializeField] private List<int> playerTiles = null;
    [Range(32, 63)]
    [SerializeField] private List<int> enemyTiles = null;

    private float roundTime = 30f;
    private float pauseModifier = 1f;

    private bool started = false;

    private CombatTeam playerHeroes, enemyHeroes = null;
    private BoardManager boardManager = null;
    private CycleManager cycleManager;
    private HeroManager heroManager;

    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
        heroManager = FindObjectOfType<HeroManager>();
        cycleManager = new CycleManager(roundTime, pauseModifier);
    }

    public void SetupCombat()
    {
        cycleManager.ResetCycle();
        ResetHeroes();
        
        CreateHeroes();
        started = true;
    }

    private void ResetHeroes()
    {
        playerHeroes = enemyHeroes = null;
        NewHero[] heroInstances = FindObjectsOfType<NewHero>();
        foreach (NewHero hero in heroInstances)
            Destroy(hero.gameObject);
    }

    void CreateHeroes()
    {
        playerHeroes = new CombatTeam(playerTiles, Team.Player, boardManager.BoardTiles, heroManager);
        enemyHeroes = new CombatTeam(enemyTiles, Team.Enemy, boardManager.BoardTiles, heroManager);
    }

    private void Update()
    {
        if (started)
            UpdateCycle();
        /*
        switch (cycleManager.State)
        {
            case CycleState.Middle:
                UpdateCycle();
                break;
        }
        */
    }

    private void UpdateCycle()
    {
        cycleManager.Update();

        switch (cycleManager.State)
        {
            case CycleState.Start:
                //start round
                break;
            case CycleState.Middle:
                //update round
                playerHeroes.SetTargetHeroes(enemyHeroes.Heroes);
                playerHeroes.Update();
                enemyHeroes.SetTargetHeroes(playerHeroes.Heroes);
                enemyHeroes.Update();
                break;
            case CycleState.End:
                //end round
                break;
        }
    }
}