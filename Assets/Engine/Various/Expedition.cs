﻿using UnityEngine;

public class Expedition
{
    public Hero hero;
    public Situation situation;
    public LocationData location;
    public LocationType destination;
    public ExpeditionPanelManager expeditionPanel;

    public Expedition(ExpeditionPanelManager expeditionPanel, Hero hero, LocationType destination)
    {
        this.expeditionPanel = expeditionPanel;
        this.hero = hero;
        this.destination = destination;
        location = Resources.Load<LocationData>("Locations/Forest");
        situation = new SituationTravelling(location);
    }

    public void Update()
    {
        if (situation.readyForNewSituation)
        {
            TryNewSituation();
        }
        else
        {
            situation.Update();
        }
    }

    public void TryNewSituation()
    {
        foreach (var sit in location.situations)
        {
            if (Random.value < sit.chance)
            {
                switch (sit.SituationType)
                {
                    case SituationType.EnemyEncounter:
                        situation = new SituationCombat(hero, location.enemies);
                        expeditionPanel.enemyPanel.enemy = (situation as SituationCombat).enemy;
                        expeditionPanel.enemyPanel.gameObject.SetActive(true);
                        break;
                    case SituationType.POIEncounter:
                        //situation = new SituationCombat(location.enemies);
                        expeditionPanel.enemyPanel.gameObject.SetActive(false);
                        break;
                    default:
                        situation = new SituationTravelling(location);
                        expeditionPanel.enemyPanel.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }
}