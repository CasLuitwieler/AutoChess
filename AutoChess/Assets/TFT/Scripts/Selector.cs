using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    BenchTile,
    BoardTile,
    Unknown
}

public class Selector : MonoBehaviour
{
    public LayerMask ClickableObjectsLayerMask;
    public LayerMask TileLayerMask;

    private Camera cam;
    private ChampionManager championManager;

    //debug
    public bool MaxChamps;

    private void Awake()
    {
        cam = Camera.main;
        championManager = FindObjectOfType<ChampionManager>();
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(mousePos, Vector3.forward, out RaycastHit hit, 10f, ClickableObjectsLayerMask))
        {
            if (hit.transform.tag == "Hero")
            {
                TileType tile = GetTileType(hit.transform.position);
                ClickedObject(tile, hit.transform.gameObject);
            }
            else if (hit.transform.tag == "HexagonTile")
            {
                ClickedObject(TileType.BoardTile, hit.transform.gameObject);
            }
            else
                ClickedObject(TileType.BenchTile, hit.transform.gameObject);
        }
    }

    public TileType GetTileType(Vector3 startPos)
    //shoot raycast down from champion position to
    //get the tile the champion is standing on
    {
        if (Physics.Raycast(startPos, Vector3.down, out RaycastHit hit, 5f, TileLayerMask))
        {
            if (hit.transform.name.Contains("Hexagon"))
                return TileType.BoardTile;
            else if (hit.transform.name.Contains("Bench"))
                return TileType.BenchTile;
            else
            {
                Debug.LogError("Champion on unknown tile");
                return TileType.Unknown;
            }
        }
        else
        {
            Debug.LogError("No tile was detected underneath champion");
            return TileType.Unknown;
        }
    }

    private void ClickedObject(TileType tileType, GameObject champ)
    {
        //if no champ selected, select the champ that was clicked
        if (!championManager.IsChampionSelected)
        {
            championManager.Select(champ);
            return;
        }

        //get tile of selected champion
        Vector3 selectedChampionPos = championManager.SelectedChampion.transform.position;
        TileType selectedChampionTile = GetTileType(selectedChampionPos);

        //max champs on board, cancel swap
        if (tileType != TileType.BenchTile && selectedChampionTile != TileType.BenchTile && MaxChamps)
        {
            Debug.Log("Invalid swap, max heroes on board");
            return;
        }

        //swap position
        championManager.SwapPosition(champ, championManager.SelectedChampion);
    }
}
