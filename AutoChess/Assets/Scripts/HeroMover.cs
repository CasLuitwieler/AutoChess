using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMover : MonoBehaviour
{
    public bool IsHeroSelected { get; private set; }

    [SerializeField] private Color sellectedHeroColor = Color.black;
    [SerializeField] private LayerMask layerMask = 0;

    private TestHero selectedHero;

    private void Awake()
    {
        IsHeroSelected = false;
    }

    private void Update()
    {
        if (IsHeroSelected && Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(selectedHero.gameObject);
            IsHeroSelected = false;
        }
    }

    public void SelectHero(TestHero hero)
    {
        if (IsHeroSelected)
            return;

        selectedHero = hero;
        hero.rend.material.color = sellectedHeroColor;
        IsHeroSelected = true;
    }

    public void PlaceHero()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 10f, layerMask.value))
        {
            //snap hero to grid
            selectedHero.transform.position = hit.transform.position;
            //reset color to hero color
            selectedHero.rend.material.color = selectedHero.standardColor;
            IsHeroSelected = false;
        }
    }
}
