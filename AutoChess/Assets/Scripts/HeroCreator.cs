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
        TestHero hero = null;

        if (team == Team.Player)
        {
            newHero = Instantiate(testHero, playerSpawn.position, Quaternion.identity, playerHeroContainer);
            hero = newHero.GetComponent<TestHero>();
            hero.SetHeroProperties(Color.green);
        }
        else if (team == Team.Enemy)
        {
            newHero = Instantiate(testHero, enemySpawn.position, Quaternion.identity, enemyHeroContainer);
            hero = newHero.GetComponent<TestHero>();
            hero.SetHeroProperties(Color.red);
        }
        newHero.layer = 8;
    }
}
