﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum AnimationTrigger
{
    KeepTravelling,
    BeginEncounter,
    EndEncounter,
    StartTransferLoot,
    Attack,
    TakeDamage
}

public class Mission
{
    internal Hero hero;
    internal Encounter curEncounter;
    internal Dictionary<ZoneData, int> route;
    internal ZoneData curZone;
    internal int zonePathProgress = 0, sitesSinceLastEncounter = 0;

    public event Action SiteChangedEvent;

    public bool GracePeriodPassed => sitesSinceLastEncounter > MissionsManager.i.minGracePeriod;

    internal Mission(MissionSetUp misSetUp)
    {
        hero = misSetUp.hero;
        route = new Dictionary<ZoneData, int>(misSetUp.route);
        curZone = route.First().Key;
    }

    public void NextAction()
    {
        if (curEncounter != null)
            curEncounter.NextAction();
        else
            NextSite();
    }

    void NextSite()
    {
        SiteChangedEvent();
        EncounterCheck();
    }

    void EncounterCheck()
    {
        if (GracePeriodPassed)
        {
            var encounter = curZone.encounters.PickOne();
            switch (encounter.type)
            {
                case EncounterType.Enemy:
                    curEncounter = new EnemyEncounter(this);
                    curEncounter.InitEncounter(this);
                    CombatStartEvent?.Invoke();
                    StartAnimation(AnimationTrigger.BeginEncounter, heroAM, encounterAM, interactionAM, locationAM);
                    break;
                case EncounterType.Container:
                    curEncounter = new ContainerEncounter();
                    curEncounter.InitEncounter(this);
                    StartAnimation(AnimationTrigger.BeginEncounter, heroAM, encounterAM);
                    break;
            }

            return;
        }
    }

    public void KeepTravelling()
    {
        Debug.Log("KeepTravelling Trigger");
        //StartAnimation(AnimationTrigger.KeepTravelling, heroAM);
    }

    // UNDO: should move somewhere?
    internal void StartAnimation(AnimationTrigger trigger, params AnimatorManager[] managers)
    {
        AnimatorManager.Trigger(trigger, managers);
    }
}