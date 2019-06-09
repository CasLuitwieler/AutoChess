using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChange;

    private CycleManager cycleManager;

    public void CreateCylceManager(float cycleTime, float pauseModifier)
    {
        cycleManager = new CycleManager(cycleTime, pauseModifier);
    }

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
        CurrentState = CurrentState = availableStates[typeof(MoveState)];
    }

    public void UpdateCurrentState()
    {
        if (CurrentState == null)
            CurrentState = availableStates[typeof(MoveState)];

        Type nextState = CurrentState?.Tick(cycleManager.CycleProgress);

        if (nextState != null && nextState != CurrentState.GetType())
            SwitchToNewState(nextState);
    }

    public void UpdateCycle()
    {
        cycleManager.Update();

        switch (cycleManager.State)
        {
            case CycleState.Start:
                CurrentState.CycleStart();
                break;
            case CycleState.Middle:
                UpdateCurrentState();
                break;
            case CycleState.End:
                CurrentState.CycleEnd();
                break;
            default:
                //do nothing
                break;
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChange?.Invoke(CurrentState);
    }
}
