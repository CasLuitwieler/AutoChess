using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewHero : MonoBehaviour
{
    //[SerializeField] private Team team = Team.None;
    //[SerializeField] private GameObject teamLight = null;

    public Transform Target { get; private set; }
    public Tile[] BoardTiles { get; private set; }
    public List<NewHero> TargetHeroes { get; private set; }
    public int CurrentTile { get; private set; }
    public int TargetMoveTile { get; set; }
    public float CycleTime { get; private set; }
    public float PauseModifier { get; private set; }
    
    public Team Team = Team.None;
    //public Team Team => team;
    public StateMachine StateMachine { get; private set; }

    private void OnEnable()
    {
        StateMachine = GetComponent<StateMachine>();
        InitializeStateMachine();
        CycleTime = 3f;
        PauseModifier = 1.5f;
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(MoveState), new MoveState(this)},
            { typeof(AttackState), new AttackState(this)},
        };

        StateMachine.SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void SetTargetHeroes(List<NewHero> targetHeroes)
    {
        TargetHeroes = targetHeroes;
    }

    public void SetCurrentTile(int currentTile)
    {
        CurrentTile = currentTile;
    }

    public void SetTiles(int currentTile, Tile[] boardTiles)
    {
        CurrentTile = TargetMoveTile = currentTile;
        BoardTiles = boardTiles;
        SetColor();
    }

    private void SetColor()
    {
        if (Team == Team.Player)
            GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
    }
}

public enum Team
{
    Player,
    Enemy,
    None
}
