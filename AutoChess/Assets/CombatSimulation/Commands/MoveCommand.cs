using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    Vector3 originalPosition;
    Vector3 targetPosition;

    public MoveCommand(IEntity entity, Vector3 targetPosition) : base(entity)
    {
        this.targetPosition = targetPosition;
    }

    public override void Execute()
    {
        originalPosition = entity.transform.position;
        entity.MoveFromTo(originalPosition, targetPosition);
    }

    public override void Undo()
    {
        entity.MoveFromTo(entity.transform.position, originalPosition);
    }
}
