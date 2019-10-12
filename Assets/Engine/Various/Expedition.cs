using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AnimationTrigger
{
    KeepTravelling,
    BeginEncounter,
    EndEncounter,
    RequiredAnimationEnded,
    StartTransferLoot,
    StopTransferLoot,
    Attack,
    TakeDamage
}

public class Expedition
{
    internal Hero hero;
    internal LocationData curLocation, destination;
    internal LocationArea curArea;
    internal int curZoneIndex;
    internal Encounter curEncounter;
    internal AnimatorManager heroAM, objectAM, interactionAM, lootAM;

    DateTime lastSituationRealTime;
    float lastSituationGameTime;

    public bool GraceTimePassed =>
        (DateTime.Now - lastSituationRealTime).TotalSeconds > ExpeditionsManager.i.minGracePeriod &&
        Time.time - lastSituationGameTime > ExpeditionsManager.i.minGracePeriod;

    LocationArea NewInterchangableArea => curLocation.areas.Find(area => area.interchangeable && area != curArea);

    bool AllAnimationsFinished =>
        heroAM.animationFinished &&
        objectAM.animationFinished &&
        lootAM.animationFinished;

    public Expedition(Hero hero, LocationData destination)
    {
        this.hero = hero;
        this.destination = destination;
        // TODO: add location transitions
        curLocation = destination;
        curArea = curLocation.areas.First();
        // "-1" to get "0" as first value later
        curZoneIndex = -1;

        InitGraceTimers();
    }

    public void Update()
    {
        // skip logic if any animation is still in progress
        if (AllAnimationsFinished)
        {
            if (curEncounter != null)
            {
                curEncounter.Update();
            }
            else
            {
                EnterNextZone();
            }
        }
    }

    public void EnterNextZone()
    {
        ChangeZoneImage();

        if (GraceTimePassed)
            NewEncounterCheck();
        else
            KeepTravelling();
    }

    //TODO: Implement log manager
    public void UpdateLog(string logEntry)
    {
        //expPanel.logPanelDrawer.AddLogEntry(logEntry);
    }

    public void InitGraceTimers()
    {
        lastSituationRealTime = DateTime.Now;
        lastSituationGameTime = Time.time;
    }

    void ChangeZoneImage()
    {
        // check if last zone in the area
        if (++curZoneIndex >= curArea.zonesPositions.Capacity)
        {
            // UNDONE : temp unsafe solution (end overflow)
            curArea = curArea.interchangeable
                ? NewInterchangableArea
                : curLocation.areas[curLocation.areas.IndexOf(curArea) + 1];
            curZoneIndex = 0;
        }
    }

    void NewEncounterCheck()
    {
        foreach (var enc in curLocation.encounters)
        {
            if (Random.value < enc.chance)
            {
                switch (enc.type)
                {
                    case EncounterType.Combat:
                        InitCombat();
                        break;
                    case EncounterType.POI:
                        //situation = new SituationCombat(location.enemies);
                        //expPanel.expDetailsPanelDrawer.enemyPanel.gameObject.SetActive(false);
                        break;
                }

                // if any situation occured, exit sequence
                return;
            }
        }
    }

    public void KeepTravelling()
    {
        Debug.Log("KeepTravelling Trigger");
        StartAnimation(AnimationTrigger.KeepTravelling, heroAM);
    }

    // NOTE: move to ExpManager?
    public void InitCombat()
    {
        StartAnimation(AnimationTrigger.BeginEncounter, heroAM, objectAM, interactionAM);
        curEncounter = new Combat(this);
    }

    // UNDO: should move somewhere?
    internal void StartAnimation(AnimationTrigger trigger, params AnimatorManager[] managers)
    {
        AnimatorManager.Trigger(trigger, managers);
    }
}