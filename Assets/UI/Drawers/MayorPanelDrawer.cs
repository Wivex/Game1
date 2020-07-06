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
    public MissionFrameDrawer expFramePrefab;
    public HeroFrameDrawer heroFramePrefab;

    #endregion

    Hero selHero;
    ZoneData selZone;

    public void InitPanel()
    {
        ClearPanels();

        // TEMP
        var locations = Resources.LoadAll<ZoneData>("Locations");
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
            // hide mis. frames in this content panel
            expContentPanel.gameObject.ChangeActiveDescending<MissionFrameDrawer>(false);
            noExpText.text = "No idle heroes available.";
            noExpText.gameObject.SetActive(true);
        }
    }

    void ClearPanels()
    {
        questContentPanel.DestroyAllChildren();
        expContentPanel.DestroyAllChildren();
    }

    // select target zone
    public void OnMissionSelect(MissionFrameDrawer mission)
    {
        selZone = mission.locData;

        // hide mis. frames in this content panel
        expContentPanel.gameObject.ChangeActiveDescending<MissionFrameDrawer>(false);

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

    // send hero on mission
    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;
        selHero.state = HeroState.OnMission;

        // hide heroFrames in this content panel
        expContentPanel.gameObject.ChangeActiveDescending<HeroFrameDrawer>(false);
        // show expFrames in this content panel
        expContentPanel.gameObject.ChangeActiveDescending<MissionFrameDrawer>(true);

        // MissionsManager.i.StartNewMission(selHero, selZone);
    }
}