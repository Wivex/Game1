using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MayorPanelDrawer : MonoBehaviour
{
    public TextMeshProUGUI noQuestsText, noExpText, timePlayedValue, sucExpValue, failedExpValue;
    public Transform questContentPanel, expContentPanel;
    public MonoBehaviour heroFramePrefab, questFramePrefab, expFramePrefab;

    List<Hero> FreeHeroes => GameManager.instance.heroes.FindAll(hero => hero.state == HeroState.InRoster);

    Hero selHero;
    LocationData selLocation;

    // initializations
    void Awake()
    {
        questContentPanel.DestroyAllChildren();
        expContentPanel.DestroyAllChildren();
    }
    
    public void InitPanel()
    {
        // NOTE: temp debug feature

        for (int i = 0; i < 3; i++)
        {
            new Hero {state = HeroState.InRoster};
        }

        foreach (var location in GameManager.instance.startingLocations)
        {
            var expPanel = expFramePrefab.Create<ExpeditionFrameDrawer>(expContentPanel);
            expPanel.Init(location);
        }
    }

    public void OnExpeditionSelect(ExpeditionFrameDrawer exp)
    {
        selLocation = exp.locData;

        // hide expFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(false);

        // init free heroes frames in the same content panel from prefabs
        foreach (var hero in FreeHeroes)
        {
            var heroPanel = heroFramePrefab.Create<HeroFrameDrawer>(expContentPanel);
            heroPanel.Init(hero);
        }
    }

    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;

        // hide heroFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<HeroFrameDrawer>(false);
        // show expFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(true);

        GameManager.instance.StartNewExpedition(selHero, selLocation);
    }
}