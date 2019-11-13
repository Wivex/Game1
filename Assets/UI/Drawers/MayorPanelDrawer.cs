using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MayorPanelDrawer : Drawer
{
    #region SET IN INSPECTOR

    public TextMeshProUGUI noQuestsText, noExpText, timePlayedValue, sucExpValue, failedExpValue;
    public Transform questContentPanel, expContentPanel;
    public ExpeditionFrameDrawer expFramePrefab;
    public HeroFrameDrawer heroFramePrefab;

    #endregion

    Hero selHero;
    LocationData selLocation;

    public void InitPanel()
    {
        ClearPanels();

        // TEMP
        var locations = Resources.LoadAll<LocationData>("Locations");
        foreach (var location in locations)
        {
            var expPanel = Instantiate(expFramePrefab, expContentPanel);
            expPanel.Init(location, this);
        }

        if (TownManager.IdleHeroes.Any())
        {
            //HACK: temp solution
            noQuestsText.text = "No available quests.";
            noQuestsText.gameObject.SetActive(true);
            noExpText.gameObject.SetActive(false);
        }
        else
        {
            noQuestsText.text = "No idle heroes available.";
            noQuestsText.gameObject.SetActive(true);
            // hide exp. frames in this content panel
            expContentPanel.gameObject.ChangeActiveDescending<ExpeditionFrameDrawer>(false);
            noExpText.text = "No idle heroes available.";
            noExpText.gameObject.SetActive(true);
        }
    }

    void ClearPanels()
    {
        questContentPanel.DestroyAllChildren();
        expContentPanel.DestroyAllChildren();
    }

    // select target location
    public void OnExpeditionSelect(ExpeditionFrameDrawer exp)
    {
        selLocation = exp.locData;

        // hide exp. frames in this content panel
        expContentPanel.gameObject.ChangeActiveDescending<ExpeditionFrameDrawer>(false);

        if (TownManager.IdleHeroes.Any())
        {
            noExpText.gameObject.SetActive(false);

            // init free heroes frames in the same content panel from prefabs
            foreach (var hero in TownManager.IdleHeroes)
            {
                var heroPanel = Instantiate(heroFramePrefab, expContentPanel);
                heroPanel.Init(hero, this);
            }
        }
        else
        {
            // enable "no free heroes text"
            noExpText.text = "No idle heroes available.";
            noExpText.gameObject.SetActive(true);
        }
    }

    // send hero on expedition
    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;
        selHero.state = HeroState.OnExpedition;

        // hide heroFrames in this content panel
        expContentPanel.gameObject.ChangeActiveDescending<HeroFrameDrawer>(false);
        // show expFrames in this content panel
        expContentPanel.gameObject.ChangeActiveDescending<ExpeditionFrameDrawer>(true);

        ExpeditionsManager.i.StartNewExpedition(selHero, selLocation);
    }
}