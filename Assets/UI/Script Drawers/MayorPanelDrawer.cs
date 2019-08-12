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
        // HACK: temp solution
        // generate first explore expedition
        //foreach (var location in GameManager.settings.startingLocations)
        //{
        //    var expPanel = expFramePrefab.Create<ExpeditionFrameDrawer>(expContentPanel);
        //    expPanel.Init(location, this);
        //}

        //if (!GameManager.IdleHeroes.Any())
        //{
        //    noQuestsText.text = "No idle heroes available.";
        //    noQuestsText.gameObject.SetActive(true);
        //    // hide exp. frames in this content panel
        //    expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(false);
        //    noExpText.text = "No idle heroes available.";
        //    noExpText.gameObject.SetActive(true);
        //}
        //else
        //{
        //    //HACK: temp solution
        //    noQuestsText.text = "No available quests.";
        //    noQuestsText.gameObject.SetActive(true);
        //    noExpText.gameObject.SetActive(false);
        //}
    }

    public void OnExpeditionSelect(ExpeditionFrameDrawer exp)
    {
        selLocation = exp.locData;

        // hide exp. frames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(false);

        //if (GameManager.IdleHeroes.Any())
        //{
        //    noExpText.gameObject.SetActive(false);

        //    // init free heroes frames in the same content panel from prefabs
        //    foreach (var hero in GameManager.IdleHeroes)
        //    {
        //        var heroPanel = heroFramePrefab.Create<HeroFrameDrawer>(expContentPanel);
        //        heroPanel.Init(hero, this);
        //    }
        //}
        //else
        //{
        //    // enable "no free heroes text"
        //    noExpText.text = "No idle heroes available.";
        //    noExpText.gameObject.SetActive(true);
        //}
    }

    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;
        selHero.state = HeroState.OnExpedition;

        // hide heroFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<HeroFrameDrawer>(false);
        // show expFrames in this content panel
        expContentPanel.SetActiveOfChildrenOfType<ExpeditionFrameDrawer>(true);

        //GameManager.settings.StartNewExpedition(selHero, selLocation);
    }
}