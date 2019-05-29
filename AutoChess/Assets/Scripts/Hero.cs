using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewHero : MonoBehaviour
{
    [SerializeField] private Team team = Team.None;
    [SerializeField] private GameObject teamLight = null;

    public Transform Target { get; private set; }

    public Team Team => team;
    public StateMachine StateMachine => GetComponent<StateMachine>();



    private void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(MoveState), new MoveState(this)},
            { typeof(AttackState), new AttackState(this)},
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}

public enum Team
{
    Friendly,
    Enemy,
    None
}

[CreateAssetMenu]
public class Hero : ScriptableObject
{
    public string Name = "New Hero";
    public HeroShape Shape = HeroShape.None;
    public HeroColor Color = HeroColor.None;
    public int Price = 0;
    public int currentTile = -1;

    public bool OnBoard { get; set; }
    public bool OnBench { get; set; }
}

public enum HeroShape
{
    Cube,
    Spere,
    Capsule,
    Cylinder,
    None
}

public enum HeroColor
{
    Red,
    Green,
    Blue,
    Magenta,
    Yellow,
    Cyan,
    Gray,
    Grey,
    None
}
