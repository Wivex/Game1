using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MayorPanelDrawer : MonoBehaviour
{
    #region SET IN INSPECTOR

    public TextMeshProUGUI noQuestsText, noExpText, timePlayedValue, sucExpValue, failedExpValue;
    public Transform questContentPanel, expContentPanel;
    public ExpeditionFrameDrawer expFramePrefab;
    public HeroFrameDrawer heroFramePrefab;

    #endregion

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
        // TEMP
        var locations = Resources.LoadAll<LocationData>("Locations");
        foreach (var location in locations)
        {
            var expPanel = Instantiate(expFramePrefab, expContentPanel);
            expPanel.Init(location, this);
        }

        if (TownManager.i.IdleHeroes.Any())
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
            expContentPanel.SetActiveForChildren<ExpeditionFrameDrawer>(false);
            noExpText.text = "No idle heroes available.";
            noExpText.gameObject.SetActive(true);
        }
    }

    // select target location
    public void OnExpeditionSelect(ExpeditionFrameDrawer exp)
    {
        selLocation = exp.locData;

        // hide exp. frames in this content panel
        expContentPanel.SetActiveForChildren<ExpeditionFrameDrawer>(false);

        if (TownManager.i.IdleHeroes.Any())
        {
            noExpText.gameObject.SetActive(false);

            // init free heroes frames in the same content panel from prefabs
            foreach (var hero in TownManager.i.IdleHeroes)
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
        expContentPanel.SetActiveForChildren<HeroFrameDrawer>(false);
        // show expFrames in this content panel
        expContentPanel.SetActiveForChildren<ExpeditionFrameDrawer>(true);

        ExpeditionManager.i.StartNewExpedition(selHero, selLocation);
    }
}