using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public List<ClickToMoveEntity> playerHeroes = new List<ClickToMoveEntity>();
    public List<ClickToMoveEntity> enemyHeroes = new List<ClickToMoveEntity>();

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
        foreach (ClickToMoveEntity hero in playerHeroes)
            hero.CalculateMove(enemyNodes);
        foreach (ClickToMoveEntity hero in enemyHeroes)
            hero.CalculateMove(playerNodes);
    }

    public void UpdateHeroNodes()
    {
        //clear previous nodes
        playerNodes.Clear();
        enemyNodes.Clear();

        //add new nodes
        foreach (ClickToMoveEntity hero in playerHeroes)
            playerNodes.Add(grid.NodeFromWorldPoint(hero.transform.position));
        foreach (ClickToMoveEntity hero in enemyHeroes)
            enemyNodes.Add(grid.NodeFromWorldPoint(hero.transform.position));
    }

    public void Move()
    {
        foreach (ClickToMoveEntity hero in playerHeroes)
            hero.Move();
        foreach (ClickToMoveEntity hero in enemyHeroes)
            hero.Move();
    }
}