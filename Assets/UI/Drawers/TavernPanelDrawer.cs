using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TavernPanelDrawer : MonoBehaviour
{
    #region SET IN INSPECTOR

    public TextMeshProUGUI noIdlesText, noRecruitsText;
    public Transform idlesContentPanel, recruitsContentPanel;
    public HeroFrameDrawer heroFramePrefab;

    #endregion

    Hero selHero;

    // initializations
    void Awake()
    {
        // HACK: temp debug feature
        for (var i = 0; i < 5; i++)
        {
            TownManager.i.CreateNewHeroDebug();
        }
    }

    public void InitPanel()
    {
        ClearPanels();

        foreach (var hero in TownManager.i.IdleHeroes)
        {
            var heroPanel = Instantiate(heroFramePrefab, idlesContentPanel);
            heroPanel.Init(hero, this);
        }

        foreach (var hero in TownManager.i.RecruitableHeroes)
        {
            var heroPanel = Instantiate(heroFramePrefab, recruitsContentPanel);
            heroPanel.Init(hero, this);
        }

        UpdateHeroesAvailabilityInfo();
    }

    void ClearPanels()
    {
        idlesContentPanel.DestroyAllChildren();
        recruitsContentPanel.DestroyAllChildren();
    }

    void UpdateHeroesAvailabilityInfo()
    {
        noIdlesText.gameObject.SetActive(!TownManager.i.IdleHeroes.Any());
        noRecruitsText.gameObject.SetActive(!TownManager.i.RecruitableHeroes.Any());
    }

    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;

        if (selHero.state == HeroState.Recruitable)
        {
            // move to idles
            heroFrame.transform.SetParent(idlesContentPanel);
            selHero.state = HeroState.Idle;
        }

        UpdateHeroesAvailabilityInfo();
    }
}