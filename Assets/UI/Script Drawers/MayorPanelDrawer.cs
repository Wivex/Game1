using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MayorPanelDrawer : MonoBehaviour
{
    public TextMeshProUGUI noQuestsText, noExpText;
    public Transform questContentPanel, expContentPanel;
    public MonoBehaviour heroFramePrefab, questFramePrefab, expFramePrefab;

    List<Hero> FreeHeroes => GameManager.instance.heroes.FindAll(hero => hero.state == HeroState.InRoster);

    // initializations
    void Awake()
    {
        questContentPanel.DestroyAllChildren();
        expContentPanel.DestroyAllChildren();
    }



    public void Init()
    {
        var freeHeroes = GameManager.instance.heroes.FindAll(hero => hero.state == HeroState.InRoster);

        if (freeHeroes.Count > 0)
        {

        }
    }

    public void OnSelectExpedition()
    {
        var freeHeroes = FreeHeroes;

        if (freeHeroes.Count > 0)
        {

        }
    }
}