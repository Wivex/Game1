using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    public event Action NewSite_Event;

    public bool GraceTimePassed => sitesSinceLastEncounter > MissionsManager.i.minGracePeriod;

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

    public void NextSite()
    {
        NewSite_Event();
        TryNewEncounter();
    }

    void ChangeZoneImage()
    {
        // // check if last zone in the area
        // if (++curZoneIndex >= curSite.zonesPositions.Capacity)
        // {
        //     // UNDONE : temp unsafe solution (end overflow)
        //     curSite = curSite.interchangeable
        //         ? NewInterchangableSite
        //         : curZone.interchangebleSites[curZone.interchangebleSites.IndexOf(curSite) + 1];
        //     curZoneIndex = 0;
        // }
    }

    void TryNewEncounter()
    {
        if (GraceTimePassed)
        {
            var encounter = curZone.encounters.PickOne();
            switch (encounter.type)
            {
                //case EncounterType.EnemyEncounter:
                //    curEncounter = new EnemyEncounter();
                //    curEncounter.InitEncounter(this);
                //    CombatStartEvent?.Invoke();
                //    StartAnimation(AnimationTrigger.BeginEncounter, heroAM, encounterAM, interactionAM, locationAM);
                //    break;
                //case EncounterType.Container:
                //    curEncounter = new ContainerEncounter();
                //    curEncounter.InitEncounter(this);
                //    StartAnimation(AnimationTrigger.BeginEncounter, heroAM, encounterAM);
                //    break;
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