using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Color whiteColor = Color.white, blackColor = Color.black;

    void Start()
    {
        SetTileColors();
    }

    void SetTileColors()
    {
        MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer>();
        int index = 0;
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            Color tileColor = meshRenderer[i].material.color;
            //increment index to start the next row with a differrent color
            if (i % 8 == 0 && i != 0)
                index++;
                
            if (index % 2 == 0)
                tileColor = whiteColor;
            else
                tileColor = blackColor;

            meshRenderer[i].material.color = tileColor;
            index++;
        }
    }
}
