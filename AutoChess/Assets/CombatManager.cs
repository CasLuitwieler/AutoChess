using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardManager = null;

    [SerializeField]
    private GameObject heroPrefab;

    private GameObject[] playerHeroes;
    private GameObject[] enemyHeroes;

    private int[] playerHeroTiles;
    private int[] enemyHeroTiles;

    [SerializeField]
    private int amountOfEnemies = 2;

    private List<HeroController> playerHeroControllers = new List<HeroController>();
    private List<HeroController> enemyHeroControllers = new List<HeroController>();
    
    public void SetupCombat()
    {
        CreateEnemies();

        GetHeroControllers();

        SetCurrentTile();
    }

    private void GetHeroControllers()
    {
        playerHeroes = boardManager.GetBoardHeroes();

        foreach (GameObject hero in playerHeroes)
        {
            if (!hero) continue;
            playerHeroControllers.Add(hero.GetComponent<HeroController>());
        }
    }

    private void SetCurrentTile()
    {
        playerHeroTiles = new int[playerHeroControllers.Count];

        for (int i = 0; i < playerHeroControllers.Count; i++)
        {
            playerHeroTiles[i] = playerHeroControllers[i].CurrentTile;
        }
    }

    private void CreateEnemies()
    {
        enemyHeroes = new GameObject[amountOfEnemies];
        for (int i = 0; i < enemyHeroes.Length; i++)
        {
            int tileNumber = Random.Range(32, 63);
            Vector3 spawnPos = boardManager.BoardTiles[tileNumber].spawnPosition;
            enemyHeroes[i] = Instantiate(heroPrefab, spawnPos, Quaternion.identity);
            enemyHeroes[i].GetComponent<HeroController>().OnTile(tileNumber);
        }
    }
}
