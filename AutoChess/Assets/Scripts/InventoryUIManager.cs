using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private GameObject benchPanel = null, boardPanel = null;
    [SerializeField]
    private Text goldText = null;

    private Inventory inventory;

    private List<GameObject> benchHeroes;
    private List<GameObject> boardHeroes;

    private Text[] benchTexts, boardTexts;
    private GameObject[] benchSlots = null, boardSlots = null;

    private void Awake()
    {
        benchTexts = benchPanel.GetComponentsInChildren<Text>();
        boardTexts = boardPanel.GetComponentsInChildren<Text>();

        Button[] benchChildren = benchPanel.GetComponentsInChildren<Button>();
        Button[] boardChildren = boardPanel.GetComponentsInChildren<Button>();

        benchSlots = new GameObject[benchChildren.Length];
        boardSlots = new GameObject[boardChildren.Length];
        
        for (int i = 0; i < benchChildren.Length; i++)
        {
            benchSlots[i] = benchChildren[i].gameObject;
            benchSlots[i].SetActive(false);
        }
        for (int i = 0; i < boardChildren.Length; i++)
        {
            boardSlots[i] = boardChildren[i].gameObject;
            boardSlots[i].SetActive(false);
        }
    }

    private void Start()
    {
        inventory = player.Inventory;
        inventory.AddGold(50);
        benchHeroes = inventory.benchHeroes;
        boardHeroes = inventory.boardHeroes;
    }

    private void Update()
    {
        goldText.text = "Gold: " + inventory.Gold.ToString();

        if(inventory.HasChanged)
        {
            UpdateInventorySlots();
            UpdateInventoryText();
            inventory.HasChanged = false;
        }
    }
    
    private void UpdateInventorySlots()
    {
        for(int i = 0; i < benchSlots.Length; i++)
        {
            int amountOfBenchHeroes = inventory.benchHeroes.Count;
            if (i < amountOfBenchHeroes && benchSlots[i].activeSelf == false)
            {
                benchSlots[i].SetActive(true);
                benchTexts[i].gameObject.SetActive(true);
            }
            else if (i >= amountOfBenchHeroes && benchSlots[i].activeSelf == true)
                benchSlots[i].SetActive(false);
        }

        for (int i = 0; i < boardSlots.Length; i++)
        {
            int amountOfBoardHeroes = inventory.boardHeroes.Count;
            if (i < amountOfBoardHeroes && boardSlots[i].activeSelf == false)
            {
                boardSlots[i].SetActive(true);
                boardTexts[i].gameObject.SetActive(true);
            }
            else if (i >= amountOfBoardHeroes && boardSlots[i].activeSelf == true)
                boardSlots[i].SetActive(false);
        }
    }

    private void UpdateInventoryText()
    {
        for(int i = 0; i < benchHeroes.Count; i++)
        {
            Hero hero = benchHeroes[i].GetComponent<HeroProperties>().Hero;
            benchTexts[i].text = hero.Name;
        }
        for (int i = 0; i < boardHeroes.Count; i++)
        {
            Hero hero = boardHeroes[i].GetComponent<HeroProperties>().Hero;
            boardTexts[i].text = hero.Name;
        }
    }
    
    public void PlaceHeroOnBoard(int HeroID)
    {
        inventory.PlaceHeroOnBoard(benchHeroes[HeroID]);
    }

    public void RetrieveHeroToBench(int HeroID)
    {
        inventory.RetrieveHeroToBench(boardHeroes[HeroID]);
    }
}
