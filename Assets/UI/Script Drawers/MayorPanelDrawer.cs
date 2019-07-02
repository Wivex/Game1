using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MayorPanelDrawer : MonoBehaviour
{
    public TextMeshProUGUI noQuestsText, noExpText, timePlayedValue, sucExpValue, failedExpValue;
    public Transform questContentPanel, expContentPanel;
    public MonoBehaviour heroFramePrefab, questFramePrefab, expFramePrefab;

    List<Hero> FreeHeroes => GameManager.instance.heroes.FindAll(hero => hero.state == HeroState.InRoster);

    // initializations
    void Awake()
    {
        questContentPanel.DestroyAllChildren();
        expContentPanel.DestroyAllChildren();
    }
    
    public void InitExpeditions()
    {
        foreach (var location in GameManager.instance.startingLocations)
        {
            var expPanel = expFramePrefab.Create<ExpeditionFrameDrawer>(expContentPanel);
            expPanel.Init(location);
        }
    }

    public void OnExpeditionSelect()
    {
        var freeHeroes = FreeHeroes;

        foreach (var hero in freeHeroes)
        {
            //var heroPanel = Instantiate(heroFramePrefab, expContentPanel);
            //heroPanel
        }
    }
}