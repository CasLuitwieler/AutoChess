using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer = -1;
    private Ray mouseRay;
    private List<IClickableEntity> selectedEntities = new List<IClickableEntity>();

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!RayHasHit(out IClickableEntity entity))
                return;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (entity.IsSelected == true)
                    Deselect(entity);
                else
                    Select(entity);
            }
            else
            {
                DeselectAll();
                Select(entity);
            }
        }

        if (Input.GetMouseButtonDown(1))
            DeselectAll();
    }

    private bool RayHasHit(out IClickableEntity entity)
    {
        entity = null;

        mouseRay = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, clickableLayer))
        {
            MonoBehaviour[] list = hit.transform.GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour obj in list)
            {
                if (obj is IClickableEntity)
                    entity = (IClickableEntity)obj;
            }
            return true;
        }
        return false;
    }

    private void Select(IClickableEntity entity)
    {
        entity.IsSelected = true;
        entity.IsChanged();
        selectedEntities.Add(entity);
    }

    private void Deselect(IClickableEntity entity)
    {
        entity.IsSelected = false;
        entity.IsChanged();
        selectedEntities.Remove(entity);
    }

    private void DeselectAll()
    {
        for(int i = selectedEntities.Count - 1; i >= 0; i--)
        {
            IClickableEntity entity = selectedEntities[i];
            entity.IsSelected = false;
            entity.IsChanged();
            selectedEntities.Remove(entity);
        }
    }
}
