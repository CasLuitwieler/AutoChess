using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(ClickInputReader))]
[RequireComponent(typeof(LineRenderer))]
public class ClickToMoveEntity : MonoBehaviour, IEntity, IClickableEntity
{
    public MeshRenderer rend { get; set; }
    public Color standardColor { get; set; }
    public bool IsSelected { get; set; }

    private SimulationHero hero;
    private CommandProcessor commandProcessor;
    public ClickInputReader ClickInputReader { get; private set; }

    private void Awake()
    {
        hero = new SimulationHero(transform);
        commandProcessor = GetComponent<CommandProcessor>();
        ClickInputReader = GetComponent<ClickInputReader>();

        IsSelected = false;
        IsChanged();
    }

    private void Update()
    {
        /*
        Vector3? clickPosition = clickInputReader.GetClickPosition();

        if (clickPosition != null)
            commandProcessor.ExecuteCommand(new MoveCommand(this, clickPosition.Value));
            */
        if (Input.GetKeyDown(KeyCode.Backspace))
            commandProcessor.Undo();
    }

    public void CalculateMove(List<Node> enemyNodes)
    {
        hero.CalculateMove(enemyNodes);
    }

    public void Move()
    {
        commandProcessor.ExecuteCommand(new MoveCommand(this, hero.targetNode.GetPosition()));
    }

    public void MoveFromTo(Vector3 startPosition, Vector3 endPosition)
    {
        transform.position = endPosition;
    }

    private void OnMouseDown()
    {
        //gameManager.SelectHero(this);
    }

    public void IsChanged()
    {
        hero.SetLineActive(IsSelected);
    }
}
