using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> availableStates;

    private Tile testTile;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChange;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    private void Update()
    {
        if (CurrentState == null)
            CurrentState = availableStates.Values.First();

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState.GetType())
            SwitchToNewState(nextState);
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChange?.Invoke(CurrentState);
    }
}
