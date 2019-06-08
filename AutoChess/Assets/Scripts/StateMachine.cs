using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChange;

    private float roundTime = 5f;
    private float roundTimeCounter;
    private bool cycleStarted = false, cycleEnded = false;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    public void UpdateCurrentState()
    {
        if (CurrentState == null)
            CurrentState = availableStates[typeof(MoveState)];

        Type nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState.GetType())
            SwitchToNewState(nextState);
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChange?.Invoke(CurrentState);
    }
}
