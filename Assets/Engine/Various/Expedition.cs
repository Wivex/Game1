using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AnimationTrigger
{
    HeroTravelling,
    BeginEncounter,
    EndEncounter,
    RequiredAnimationEnded,
    StartTransferLoot,
    StopTransferLoot
}

public class Expedition
{
    internal Hero hero;
    internal LocationData curLocation, destination;
    internal LocationArea curArea;
    internal int curZoneIndex;
    internal Encounter curEncounter;
    internal AnimationStateReference anyAnimator = new AnimationStateReference();
    internal ExpeditionRedrawFlags redrawFlags;
    internal AnimationManager heroAM, objectAM, interactionAM, lootAM;

    DateTime lastSituationRealTime;
    float lastSituationGameTime;

    public bool GraceTimePassed =>
        (DateTime.Now - lastSituationRealTime).TotalSeconds > ExpeditionsManager.i.minGracePeriod &&
        Time.time - lastSituationGameTime > ExpeditionsManager.i.minGracePeriod;

    LocationArea NewInterchangableArea => curLocation.areas.Find(area => area.interchangeable && area != curArea);

    public Expedition(Hero hero, LocationData destination)
    {
        this.hero = hero;
        this.destination = destination;
        // TODO: add location transitions
        curLocation = destination;
        curArea = curLocation.areas.First();
        // "-1" to get "0" as first value later
        curZoneIndex = -1;
        anyAnimator.state = AnimationState.Finished;

        InitGraceTimers();
    }

    public void Update()
    {
        // skip logic if any animation is still in progress
        if (anyAnimator.state == AnimationState.InProgress) return;

        if (curEncounter != null)
        {
            curEncounter.Update();
        }
        else
        {
            // continue travelling
            EnterNextZone();
        }
    }

    public void EnterNextZone()
    {
        ChangeZoneImage();

        if (GraceTimePassed)
            NewEncounterCheck();
        else
            InitTravelling();   
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
            curArea = curArea.interchangeable ? NewInterchangableArea : curLocation.areas[curLocation.areas.IndexOf(curArea)+1];
            curZoneIndex = 0;
        }

        // set flag to redraw zone 
        redrawFlags.zone = true;
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

    // NOTE: move to ExpManager?
    public void InitTravelling()
    {
        Debug.Log($"{hero.name} triggered {AnimationTrigger.HeroTravelling.ToString()}");
        //start hero travelling animation
        AnimationManager.Trigger(AnimationTrigger.HeroTravelling, heroAM);
        anyAnimator.state = AnimationState.InProgress;
        curEncounter = null;
    }

    // NOTE: move to ExpManager?
    public void InitCombat()
    {
        Debug.Log($"{hero.name} triggered {AnimationTrigger.BeginEncounter.ToString()}");
        AnimationManager.Trigger(AnimationTrigger.BeginEncounter, heroAM, objectAM, interactionAM);
        anyAnimator.state = AnimationState.InProgress;
        curEncounter = new Combat(this);
    }
}