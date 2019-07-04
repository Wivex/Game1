using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TavernPanelDrawer : MonoBehaviour
{
    public TextMeshProUGUI noIdlesText, noRecruitsText;
    public Transform idlesContentPanel, recruitsContentPanel;
    public MonoBehaviour heroFramePrefab;

    List<Hero> FreeHeroes => GameManager.instance.heroes.FindAll(hero => hero.state == HeroState.InRoster);

    // initializations
    void Awake()
    {
        idlesContentPanel.DestroyAllChildren();
        recruitsContentPanel.DestroyAllChildren();
    }

    public void Init()
    {
        //var freeHeroes = GameManager.instance.heroes.FindAll(hero => hero.state == HeroState.InRoster);

        //if (freeHeroes.Count > 0)
        //{

        //}
    }

    public void OnHeroSelect()
    {
        var freeHeroes = FreeHeroes;

        if (freeHeroes.Count > 0)
        {

        }
    }
}