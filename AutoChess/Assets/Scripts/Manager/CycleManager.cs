using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager
{
    public CycleState State { get; private set; }
    public float CycleProgress { get; private set; }

    private float cycleTime;
    private float cycleTimeCounter;
    private float pauseModifier;
    private bool cycleStarted = false, cycleEnded = false;

    public CycleManager(float cycleTime, float pauseModifier)
    {
        this.cycleTime = cycleTime;
        this.pauseModifier = pauseModifier;
    }

    public void Update()
    {
        cycleTimeCounter += Time.deltaTime;
        CycleProgress = Mathf.Clamp(cycleTimeCounter / (cycleTime / pauseModifier), 0, 1);

        if (cycleTimeCounter <= cycleTime / 2 && !cycleStarted)
        {
            State = CycleState.Start;
            cycleStarted = true;
        }
        else if (cycleTimeCounter <= cycleTime / pauseModifier)
            State = CycleState.Middle;
        else if (cycleTimeCounter > cycleTime / 1.2f && !cycleEnded)
        {
            State = CycleState.End;
            cycleEnded = true;
        }
        if (cycleTimeCounter >= cycleTime)
            ResetCycle();
    }

    public void ResetCycle()
    {
        cycleTimeCounter = 0;
        cycleStarted = false;
        cycleEnded = false;
    }
}

public enum CycleState
{
    Start,
    Middle,
    End,
    Finished,
    None
}