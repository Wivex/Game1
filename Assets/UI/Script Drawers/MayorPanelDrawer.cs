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
        foreach (var location in GameManager.instance.startingLocations)
        {
            var expPanel = expFramePrefab.Create<ExpeditionFrameDrawer>(expContentPanel);
            expPanel.Init(location, this);
        }
    }

    public void OnExpeditionSelect(ExpeditionFrameDrawer exp)
    {
        selLocation = exp.locData;

        // hide expFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(false);

        // init free heroes frames in the same content panel from prefabs
        foreach (var hero in GameManager.IdleHeroes)
        {
            var heroPanel = heroFramePrefab.Create<HeroFrameDrawer>(expContentPanel);
            heroPanel.Init(hero, this);
        }
    }

    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;
        selHero.state = HeroState.OnExpedition;

        // hide heroFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<HeroFrameDrawer>(false);
        // show expFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(true);

        GameManager.instance.StartNewExpedition(selHero, selLocation);
    }
}