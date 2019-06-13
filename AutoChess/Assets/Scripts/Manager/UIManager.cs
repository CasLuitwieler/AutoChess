using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPanel = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
