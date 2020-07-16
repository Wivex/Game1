using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TavernPanelDrawer : Drawer
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
            TownManager.GenerateRandomHeroRecruit();
        }
    }

    public void InitPanel()
    {
        ClearPanels();

        foreach (var hero in TownManager.IdleHeroes)
        {
            var heroPanel = Instantiate(heroFramePrefab, idlesContentPanel);
            heroPanel.Init(hero);
        }

        foreach (var hero in TownManager.RecruitableHeroes)
        {
            var heroPanel = Instantiate(heroFramePrefab, recruitsContentPanel);
            heroPanel.Init(hero);
            heroPanel.button.onClick.AddListener(() => OnHeroSelect(heroPanel));
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
        noIdlesText.gameObject.SetActive(!TownManager.IdleHeroes.Any());
        noRecruitsText.gameObject.SetActive(!TownManager.RecruitableHeroes.Any());
    }

    public void OnHeroSelect(HeroFrameDrawer heroFrame)
    {
        selHero = heroFrame.hero;

        if (selHero.state == HeroState.Recruit)
        {
            // move to idles
            heroFrame.transform.SetParent(idlesContentPanel);
            selHero.state = HeroState.Idle;
        }

        UpdateHeroesAvailabilityInfo();
    }
}