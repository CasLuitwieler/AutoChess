using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInputReader : MonoBehaviour
{
    [SerializeField] private LayerMask nodeMask = -1;
    private bool selected;

    public Vector3? GetClickPosition()
    {
        //check for click
        if (!Input.GetMouseButtonDown(0))
            return null;

        //ray from mousePosition
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, nodeMask))
        {
            if(hit.transform.GetComponent<Node>())
            {
                //return node position
                Node node = hit.transform.GetComponent<Node>();
                return node.GetPosition();
            }
        }
        return null;
    }
}
