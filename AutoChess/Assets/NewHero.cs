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
    public List<int> TargetTiles { get; private set; }
    public int CurrentTile { get; private set; }
    public int TargetTile { get; set; }
    public int TargetMoveTile { get; set; }

    public Team Team = Team.None;
    //public Team Team => team;
    public StateMachine StateMachine { get; private set; }

    private void OnEnable()
    {
        StateMachine = GetComponent<StateMachine>();
        InitializeStateMachine();
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

    public void SetCurrentTile(int currentTile)
    {
        CurrentTile = currentTile;
    }

    public void SetTiles(Tile[] boardTiles, List<int> targetTiles)
    {
        BoardTiles = boardTiles;
        TargetTiles = targetTiles;
    }
}

public enum Team
{
    Friendly,
    Enemy,
    None
}
