using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected IEntity entity;

    public Command(IEntity entity)
    {
        this.entity = entity;
    }

    public abstract void Execute();
    public abstract void Undo();
}
