using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChange;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
        CurrentState = CurrentState = availableStates[typeof(MoveState)];
    }

    public void UpdateCurrentState(float roundTime)
    {
        if (CurrentState == null)
            CurrentState = availableStates[typeof(MoveState)];

        Type nextState = CurrentState?.Tick(roundTime);

        if (nextState != null && nextState != CurrentState.GetType())
            SwitchToNewState(nextState);
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChange?.Invoke(CurrentState);
    }
}
