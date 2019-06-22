using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCreator : MonoBehaviour
{
    [SerializeField] private GameObject testHero = null;
    [SerializeField] private Transform playerHeroContainer = null, enemyHeroContainer = null;
    [SerializeField] private Transform playerSpawn = null, enemySpawn = null;
    [SerializeField] private Button spawnPlayerHeroButton = null, spawnEnemyHeroButton = null;

    private void Awake()
    {
        spawnPlayerHeroButton.onClick.AddListener(() => CreateHero(Team.Player));
        spawnEnemyHeroButton.onClick.AddListener(() => CreateHero(Team.Enemy));
    }

    private void CreateHero(Team team)
    {
        GameObject newHero = null;

        if (team == Team.Player)
        {
            newHero = Instantiate(testHero, playerSpawn.position, Quaternion.identity, playerHeroContainer);
            InitializeHero(newHero, Color.green);
        }
        else if (team == Team.Enemy)
        {
            newHero = Instantiate(testHero, enemySpawn.position, Quaternion.identity, enemyHeroContainer);
            InitializeHero(newHero, Color.red);
        }
        newHero.layer = 8;
    }

    private void InitializeHero(GameObject hero, Color color)
    {
        TestHero testHero = hero.GetComponent<TestHero>();
        testHero.standardColor = color;
        testHero.GetComponentInChildren<MeshRenderer>().material.color = color;
    }
}
