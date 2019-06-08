using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager = null;

    [SerializeField]
    private GameObject heroPrefab = null;

    [Range(0, 31)]
    [SerializeField] private List<int> playerTiles = null;
    [Range(32, 63)]
    [SerializeField] private List<int> enemyTiles = null;

    private List<NewHero> playerHeroes;
    private List<NewHero> enemyHeroes;
    private List<StateMachine> heroStateMachines;

    private float roundTime = 5f;
    private float roundTimeCounter;
    private bool cycleStarted = false, cycleEnded = false;

    private RoundState roundState = RoundState.None;

    public void SetupCombat()
    {
        ResetCombat();
        CreateHeroes();

        roundTimeCounter = 0;
        roundState = RoundState.Start;
    }

    private void ResetCombat()
    {
        ResetCycle();
        playerHeroes = null;
        enemyHeroes = null;
        heroStateMachines = null;    
    }

    private void CreateHeroes()
    {
        playerHeroes = new List<NewHero>();
        enemyHeroes = new List<NewHero>();
        heroStateMachines = new List<StateMachine>();

        for (int i = 0; i < playerTiles.Count; i++)
        {
            //get tileNumber
            int tileNumber = playerTiles[i]; //int tileNumber = Random.Range(0, 31);
            //get hero spawnPosition
            Vector3 spawnPos = boardManager.BoardTiles[tileNumber].spawnPosition;
            //create hero
            GameObject newHero = Instantiate(heroPrefab, spawnPos, Quaternion.identity);
            playerHeroes.Add(newHero.GetComponent<NewHero>());
            //set tileNumber
            playerHeroes[i].SetCurrentTile(tileNumber);
            //assign team
            playerHeroes[i].Team = Team.Friendly;
            //add hero to stateMachines
            heroStateMachines.Add(playerHeroes[i].GetComponent<StateMachine>());
        }
        for (int i = 0; i < enemyTiles.Count; i++)
        {
            //get tileNumber
            int tileNumber = enemyTiles[i]; //int tileNumber = Random.Range(32, 63);
            //get hero spawnPosition
            Vector3 spawnPos = boardManager.BoardTiles[tileNumber].spawnPosition;
            //create hero
            enemyHeroes.Add(Instantiate(heroPrefab, spawnPos, Quaternion.identity).GetComponent<NewHero>());
            //set tileNumber
            enemyHeroes[i].SetCurrentTile(tileNumber);
            //assign team
            enemyHeroes[i].Team = Team.Enemy;
        }
        foreach (NewHero hero in playerHeroes)
            hero.SetTiles(boardManager.BoardTiles, enemyTiles);
        foreach (NewHero hero in enemyHeroes)
            hero.SetTiles(boardManager.BoardTiles, playerTiles);
    }

    private void Update()
    {
        roundTimeCounter += Time.deltaTime;

        if(roundState == RoundState.Start)
            UpdateCycle();
    }

    private void UpdateCycle()
    {
        if (roundTimeCounter <= roundTime / 2 && !cycleStarted)
        {
            foreach (StateMachine stateMachine in heroStateMachines)
                stateMachine.CurrentState.CycleStart();
            cycleStarted = true;
        }
        else if (roundTimeCounter <= roundTime / 2)
        {
            foreach (StateMachine stateMachine in heroStateMachines)
                stateMachine.UpdateCurrentState(roundTimeCounter / (roundTime/2));
        }
        else if (roundTimeCounter > roundTime / 2 && !cycleEnded)
        {
            foreach (StateMachine stateMachine in heroStateMachines)
                stateMachine.CurrentState.CycleEnd();
            cycleEnded = true;
        }
        if (roundTimeCounter >= roundTime)
            ResetCycle();
    }

    private void ResetCycle()
    {
        roundTimeCounter = 0;
        cycleStarted = false;
        cycleEnded = false;
    }
}

public enum RoundState
{
    Start,
    Combat,
    BuyTime,
    GameOver,
    None
}
