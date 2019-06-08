using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;

    public BaseState(NewHero hero)
    {
        this.gameObject = hero.gameObject;
        this.transform = gameObject.transform;
    }

    public virtual void CycleStart() { }
    public abstract Type Tick();
    public virtual void CycleEnd() { }
}
