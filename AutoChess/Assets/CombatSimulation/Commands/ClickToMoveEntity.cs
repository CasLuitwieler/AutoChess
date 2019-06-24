using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(ClickInputReader))]
[RequireComponent(typeof(LineRenderer))]
public class ClickToMoveEntity : MonoBehaviour, IEntity
{
    public MeshRenderer rend { get; set; }
    public Color standardColor { get; set; }

    private TestHero hero;
    private CommandProcessor commandProcessor;
    private ClickInputReader clickInputReader;    

    private void Awake()
    {
        hero = new TestHero(transform);
        commandProcessor = GetComponent<CommandProcessor>();
        clickInputReader = GetComponent<ClickInputReader>();
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
}
