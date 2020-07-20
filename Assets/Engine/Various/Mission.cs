using System;
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
            switch (curZone.encounters.PickOne().type)
            {
                case EncounterType.None:
                    curEncounter = null;
                    break;
                case EncounterType.Enemy:
                    curEncounter = new EnemyEncounter(this);
                    break;
            }
        }
    }

    public void KeepTravelling()
    {
        Debug.Log("KeepTravelling Trigger");
        //StartAnimation(AnimationTrigger.KeepTravelling, heroAM);
    }

    // UNDO: should move somewhere?
    internal void StartAnimation(AnimationTrigger trigger, params AnimationManager[] managers)
    {
        AnimationManager.Trigger(trigger, managers);
    }
}