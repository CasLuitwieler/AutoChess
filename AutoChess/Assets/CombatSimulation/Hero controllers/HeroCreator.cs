using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCreator : MonoBehaviour
{
    [SerializeField] private GameObject heroGO = null;
    [SerializeField] private Transform playerHeroContainer = null, enemyHeroContainer = null;
    [SerializeField] private Transform playerSpawn = null, enemySpawn = null;
    [SerializeField] private Button spawnPlayerHeroButton = null, spawnEnemyHeroButton = null;

    private MoveManager moveManager;

    private void Awake()
    {
        spawnPlayerHeroButton.onClick.AddListener(() => CreateHero(Team.Player));
        spawnEnemyHeroButton.onClick.AddListener(() => CreateHero(Team.Enemy));

        moveManager = FindObjectOfType<MoveManager>();
    }

    private void CreateHero(Team team)
    {
        GameObject newHero = null;

        if (team == Team.Player)
        {
            newHero = Instantiate(heroGO, playerSpawn.position, Quaternion.identity, playerHeroContainer);
            InitializeHero(newHero, Color.green);
            moveManager.playerHeroes.Add(newHero.GetComponent<ClickToMoveEntity>());
        }
        else if (team == Team.Enemy)
        {
            newHero = Instantiate(heroGO, enemySpawn.position, Quaternion.identity, enemyHeroContainer);
            InitializeHero(newHero, Color.red);
            moveManager.enemyHeroes.Add(newHero.GetComponent<ClickToMoveEntity>());
        }
        newHero.layer = 8;
    }

    private void InitializeHero(GameObject heroGO, Color color)
    {
        ClickToMoveEntity hero = heroGO.GetComponent<ClickToMoveEntity>();
        hero.standardColor = color;
        hero.GetComponentInChildren<MeshRenderer>().material.color = color;
    }
}
