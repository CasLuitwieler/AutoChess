using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    private List<TestHero> playerHeroes = new List<TestHero>();
    private List<TestHero> enemyHeroes = new List<TestHero>();

    private List<Node> playerNodes = new List<Node>();
    private List<Node> enemyNodes = new List<Node>();

    private NodeGrid grid;

    private void Awake()
    {
        grid = FindObjectOfType<NodeGrid>();
    }

    public void CalculateMove()
    {
        UpdateHeroNodes();

        //calculate hero move using the opponents hero nodes
        foreach (TestHero hero in playerHeroes)
            hero.CalculateMove(enemyNodes);
        foreach (TestHero hero in enemyHeroes)
            hero.CalculateMove(playerNodes);
    }

    public void UpdateHeroNodes()
    {
        //clear previous nodes
        playerNodes.Clear();
        enemyNodes.Clear();

        //add new nodes
        foreach (TestHero hero in playerHeroes)
            playerNodes.Add(grid.NodeFromWorldPoint(hero.transform.position));
        foreach (TestHero hero in enemyHeroes)
            enemyNodes.Add(grid.NodeFromWorldPoint(hero.transform.position));
    }

    public void Move()
    {
        foreach (TestHero hero in playerHeroes)
            hero.Move();
        foreach (TestHero hero in enemyHeroes)
            hero.Move();
    }
}

struct HeroTile
{
    public int X, Y;
    public Vector3 WorldPosition;

    public HeroTile(int x, int y, Vector3 worldPosition)
    {
        X = x;
        Y = y;
        WorldPosition = worldPosition;
    }
}