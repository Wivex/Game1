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

public enum ExpEventType
{
    None,
    Combat,
    POI
}

public class Expedition
{
    internal Hero hero;
    internal LocationData curLocation, destination;
    internal LocationArea curArea;
    internal int curZoneIndex;
    internal ExpEventType curEventType;
    internal AnimationStateReference animStateRef = new AnimationStateReference();
    internal ExpeditionRedrawFlags redrawFlags = new ExpeditionRedrawFlags();
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
        animStateRef.state = AnimationState.Finished;
        curEventType = ExpEventType.None;

        InitGraceTimers();
    }

    public void Update()
    {
        //TODO: should replace situation updates here
        if (animStateRef.state == AnimationState.Finished)
            EnterNextZone();
    }

    public void EnterNextZone()
    {
        ChangeZoneImage();

        if (GraceTimePassed)
            TryNewEvent();
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
            curArea = NewInterchangableArea;
            curZoneIndex = 0;
        }

        // set flag to redraw zone 
        redrawFlags.zone = true;
    }

    void TryNewEvent()
    {
        foreach (var e in curLocation.events)
        {
            if (Random.value < e.chance)
            {
                switch (e.eventType)
                {
                    case ExpEventType.Combat:
                        InitCombat();
                        break;
                    case ExpEventType.POI:
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
        heroAM.SetTrigger(AnimationTrigger.HeroTravelling.ToString());
    }

    public void InitCombat()
    {
        //var enemy = (situation as CombatManager).enemy;
        //UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.InitEnemyPanel(enemy);
        //UpdateLog($"{hero.name} started combat with {enemy.enemyData.name}");
        //Debug.Log($"{hero.name} triggered {AnimationTrigger.BeginEncounter.ToString()}");
        //expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        //expPreviewPanel.eventAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        //expPreviewPanel.interAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        //// hide "dead" status icon
        //expPreviewPanel.enemyStatusIcon.enabled = false;
    }
}