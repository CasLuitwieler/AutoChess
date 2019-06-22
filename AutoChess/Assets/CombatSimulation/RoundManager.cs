using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private Button nextStateButton = null;

    public RoundState RoundState { get; private set; }
    public bool StateChanged { get; private set; }

    private MoveManager moveManager;

    private void Awake()
    {
        moveManager = GetComponent<MoveManager>();
        nextStateButton.onClick.AddListener(() => ChangeState());
        RoundState = RoundState.None;
    }

    private void ChangeState()
    {
        switch(RoundState)
        {
            case RoundState.None:
                RoundState = RoundState.Start;
                moveManager.CalculateMove();
                break;
            case RoundState.Start:
                RoundState = RoundState.Move;
                moveManager.Move();
                break;
            case RoundState.Move:
                RoundState = RoundState.Start;
                moveManager.CalculateMove();
                break;
        }
    }
}

public enum RoundState
{
    Start,
    Move,
    None
}

