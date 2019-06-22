using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(ClickInputReader))]
public class ClickToMoveEntity : MonoBehaviour, IEntity
{
    private CommandProcessor commandProcessor;
    private ClickInputReader clickInputReader;

    private void Awake()
    {
        commandProcessor = GetComponent<CommandProcessor>();
        clickInputReader = GetComponent<ClickInputReader>();
    }

    private void Update()
    {
        Vector3? clickPosition = clickInputReader.GetClickPosition();

        if (clickPosition != null)
            commandProcessor.ExecuteCommand(new MoveCommand(this, clickPosition.Value));
        if (Input.GetKeyDown(KeyCode.Backspace))
            commandProcessor.Undo();
    }

    public void MoveFromTo(Vector3 startPosition, Vector3 endPosition)
    {
        transform.position = endPosition;
    }
}
