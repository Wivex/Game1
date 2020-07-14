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
    internal Dictionary<ZoneData, int> route = new Dictionary<ZoneData, int>();
    //internal ZoneData curZone, destination;
    //internal Site curSite;
    //internal int curZoneIndex;
    internal Encounter curEncounter;
    internal AnimatorManager heroAM, encounterAM, interactionAM, lootAM, locationAM;

    DateTime lastSituationRealTime;
    float lastSituationGameTime;

    //public event Action CombatStartEvent;

    public bool GraceTimePassed =>
        //(DateTime.Now - lastSituationRealTime).TotalSeconds > MissionsManager.i.minGracePeriod &&
        Time.time - lastSituationGameTime > MissionsManager.i.minGracePeriod;

    //Site NewInterchangableSite => curZone.sites.Find(site => site != curSite && site.type == SiteType.Interchangeble);

    bool AllAnimationsFinished =>
        heroAM.animationFinished &&
        encounterAM.animationFinished &&
        lootAM.animationFinished;

    internal Mission(MissionSetUp misSetUp)
    {
        hero = misSetUp.hero;
        route = new Dictionary<ZoneData, int>(misSetUp.route);
    }

    public void Update()
    {
        // skip logic if any animation is still in progress
        if (AllAnimationsFinished)
        {
            if (curEncounter != null)
                curEncounter.NextStage();
            else
                EnterNextZone();
        }
    }

    public void EnterNextZone()
    {
        ChangeZoneImage();
        TryNewEncounter();
    }

    public void InitGraceTimers()
    {
        lastSituationRealTime = DateTime.Now;
        lastSituationGameTime = Time.time;
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
        // if (GraceTimePassed)
        // {
        //     foreach (var enc in curZone.encounters)
        //     {
        //         if (Random.value < enc.chanceWeight)
        //         {
        //             switch (enc.type)
        //             {
        //                 case EncounterType.EnemyEncounter:
        //                     curEncounter = new EnemyEncounter();
        //                     curEncounter.InitEncounter(this);
        //                     CombatStartEvent?.Invoke();
        //                     StartAnimation(AnimationTrigger.BeginEncounter, heroAM, encounterAM, interactionAM, locationAM);
        //                     break;
        //                 case EncounterType.Container:
        //                     curEncounter = new ContainerEncounter();
        //                     curEncounter.InitEncounter(this);
        //                     StartAnimation(AnimationTrigger.BeginEncounter, heroAM, encounterAM);
        //                     break;
        //             }
        //             return;
        //         }
        //     }
        // }

        // if no new encounter, keep travelling
        KeepTravelling();
    }

    public void KeepTravelling()
    {
        Debug.Log("KeepTravelling Trigger");
        StartAnimation(AnimationTrigger.KeepTravelling, heroAM);
    }

    // UNDO: should move somewhere?
    internal void StartAnimation(AnimationTrigger trigger, params AnimatorManager[] managers)
    {
        AnimatorManager.Trigger(trigger, managers);
    }
}