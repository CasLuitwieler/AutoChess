using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager = null;

    [SerializeField]
    private GameObject heroPrefab = null;

    private List<int> playerHeroTiles;
    private List<int> enemyHeroTiles = new List<int>();

    [SerializeField] private int[] playerTiles;
    [SerializeField] private int[] enemyTiles;

    private List<NewHero> playerHeroes;
    private List<NewHero> enemyHeroes;
    private List<StateMachine> heroStateMachines;

    private float roundTime = 5f;
    private float roundTimeCounter;
    private bool cycleStarted = false, cycleEnded = false;

    public void SetupCombat()
    {
        ResetCombat();
        CreateHeroes();

        roundTimeCounter = 0;
    }

    private void ResetCombat()
    {
        ResetCycle();
        playerHeroes = null;
        enemyHeroes = null;
        heroStateMachines = null;
        playerHeroTiles = null;
        enemyHeroTiles = null;
    }

    private void CreateHeroes()
    {
        playerHeroes = new List<NewHero>();
        enemyHeroes = new List<NewHero>();
        heroStateMachines = new List<StateMachine>();
        playerHeroTiles = new List<int>();
        enemyHeroTiles = new List<int>();

        for (int i = 0; i < playerTiles.Length; i++)
        {
            //get tileNumber
            int tileNumber = playerTiles[i]; //int tileNumber = Random.Range(0, 31);
            //get hero spawnPosition
            Vector3 spawnPos = boardManager.BoardTiles[tileNumber].spawnPosition;
            //create hero
            playerHeroes[i] = Instantiate(heroPrefab, spawnPos, Quaternion.identity).GetComponent<NewHero>();
            //set tileNumber
            playerHeroes[i].SetCurrentTile(tileNumber);
            playerHeroTiles.Add(tileNumber);
            //add hero to stateMachines
            heroStateMachines.Add(playerHeroes[i].GetComponent<StateMachine>());
        }
        for (int i = 0; i < enemyTiles.Length; i++)
        {
            //get tileNumber
            int tileNumber = enemyTiles[i]; //int tileNumber = Random.Range(32, 63);
            //get hero spawnPosition
            Vector3 spawnPos = boardManager.BoardTiles[tileNumber].spawnPosition;
            //create hero
            enemyHeroes[i] = Instantiate(heroPrefab, spawnPos, Quaternion.identity).GetComponent<NewHero>();
            //set tileNumber
            enemyHeroes[i].SetCurrentTile(tileNumber);
            enemyHeroTiles.Add(tileNumber);
        }
    }

    private void Update()
    {
        roundTimeCounter += Time.deltaTime;

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
                stateMachine.UpdateCurrentState();
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
        cycleEnded = false;
        cycleEnded = false;
    }
}
