using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionManager : MonoBehaviour
{
    public GameObject SelectedChampion { get; private set; }

    public bool IsChampionSelected
    {
        get { return SelectedChampion != null; }
    }
    
    public void Deselect()
    {
        SelectedChampion = null;
    }

    public void Select(GameObject champ)
    {
        SelectedChampion = champ;
    }
    
    public void SwapPosition(GameObject champ1, GameObject champ2)
    {
        Vector3 temp = champ1.transform.position;
        champ1.transform.position = champ2.transform.position;
        champ2.transform.position = temp;
    }
}
