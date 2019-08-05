using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TavernPanelDrawer : MonoBehaviour
{
    public TextMeshProUGUI noIdlesText, noRecruitsText;
    public Transform idlesContentPanel, recruitsContentPanel;
    public MonoBehaviour heroFramePrefab;

    Hero selHero;

    // initializations
    void Awake()
    {
        idlesContentPanel.DestroyAllChildren();
        recruitsContentPanel.DestroyAllChildren();

        // NOTE: temp debug feature
        for (var i = 0; i < 3; i++)
        {
            new Hero();
        }
    }

    public void InitPanel()
    {
        foreach (var hero in GameManager.IdleHeroes)
        {
            var heroPanel = heroFramePrefab.Create<HeroFrameDrawer>(idlesContentPanel);
            heroPanel.Init(hero, this);
        }

        foreach (var hero in GameManager.RecruitableHeroes)
        {
            var heroPanel = heroFramePrefab.Create<HeroFrameDrawer>(recruitsContentPanel);
            heroPanel.Init(hero, this);
        }
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
    }
}