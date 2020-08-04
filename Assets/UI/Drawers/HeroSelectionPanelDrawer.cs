using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HeroSelectionPanelDrawer : Drawer
{
    #region SET IN INSPECTOR

    public Transform heroesListPanel;
    public HeroFrameDrawer heroFramePrefab;
    public GameObject noHeroesMessage;
    public TextMeshProUGUI heroStats;

    #endregion

    Hero selHero;
    List<HeroFrameDrawer> heroPanels;

    public void InitPanel()
    {
        // remove old hero panels
        heroesListPanel.DestroyAllChildren();

        heroStats.text = "No hero selected";

        if (TownManager.IdleHeroes.Any())
        {
            // hide message
            noHeroesMessage.SetActive(false);

            foreach (var hero in TownManager.IdleHeroes)
            {
                var heroPanel = Instantiate(heroFramePrefab, heroesListPanel);
                heroPanel.Init(hero);
                heroPanel.button.onClick.AddListener(()=>OnHeroSelect(hero));
            }
        }
        else
        {
            // show message
            noHeroesMessage.SetActive(true);
        }
    }

    // send hero on mission
    public void OnHeroSelect(Hero hero)
    {
        selHero = hero;
        MissionsManager.missionSetUp.hero = selHero;

        // TODO: update hero info
        heroStats.text = $@"{selHero.Name} stats";
    }
}