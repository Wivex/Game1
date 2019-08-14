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

public enum SituationType
{
    Travelling,
    EnemyEncounter,
    ObjectEncounter
}

public class Expedition
{
    internal Hero hero;
    internal LocationData curLocation, destination;
    internal LocationArea curArea;
    internal int curZoneIndex;
    internal SituationType type;
    internal AnimationStateReference animStateRef;
    internal ExpeditionRedrawFlags redrawFlags;

    DateTime lastSituationRealTime;
    float lastSituationGameTime;

    public Expedition(Hero hero, LocationData destination)
    {
        this.hero = hero;
        //UIManager.instance.expPanelDrawer.NewPreviewPanel(this);
        //hero.unitPreviewIcon = expPreviewPanel.heroIcon.transform;
        //hero.unitDetailsIcon = UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.heroPanel.unitImage.transform;
        this.destination = destination;
        // TODO: add location transitions
        curLocation = destination;
        curArea = curLocation.areas.First();
        // "-1" to get "0" as first value later
        curZoneIndex = -1;

        ResetGraceTimers();
        // TODO: should not run logic at initialization
        //TryNewSituation();
    }

    LocationArea NewInterchangableArea => curLocation.areas.Find(area => area.interchangeable && area != curArea);

    //public bool GraceTimePassed =>
    //    (DateTime.Now - lastSituationRealTime).TotalSeconds > GameManager.settings.minGracePeriod &&
    //    Time.time - lastSituationGameTime > GameManager.settings.minGracePeriod;

    public void Update()
    {
        //if (situation.state == SituationState.RunningLogic)
        //    situation.Update();
    }

    public void UpdateLog(string logEntry)
    {
        //expPanel.logPanelDrawer.AddLogEntry(logEntry);
    }

    public void ResetGraceTimers()
    {
        lastSituationRealTime = DateTime.Now;
        lastSituationGameTime = Time.time;
    }

    public void AnimationEnded()
    {
        //situation.state = SituationState.RunningLogic;
    }

    public void TryNewSituation()
    {
        NextZone();
        NextSituation();
    }

    void NextZone()
    {
        // check if last zone in the area
        if (++curZoneIndex >= curArea.zonesPositions.Capacity)
        {
            curArea = NewInterchangableArea;
            curZoneIndex = 0;
        }

        // set flag to redraw zone 
        //expPreviewPanel.redrawFlags.zone = true;
    }

    void NextSituation()
    {
        //if (GraceTimePassed)
        {
            foreach (var sit in curLocation.situations)
            {
                if (Random.value < sit.chance)
                {
                    switch (sit.type)
                    {
                        case SituationType.EnemyEncounter:
                            InitEnemyEncounterSituation();
                            break;
                        case SituationType.ObjectEncounter:
                            //situation = new SituationCombat(location.enemies);
                            //expPanel.expDetailsPanelDrawer.enemyPanel.gameObject.SetActive(false);
                            break;
                    }

                    // if any situation occured, exit sequence
                    return;
                }
            }
        }
        // too early for new situation, continue travelling
        //else
        {
            // if not already travelling
            //if (situation?.type != SituationType.Travelling)
                InitTravellingSituation();
        }
    }

    public void InitTravellingSituation()
    {
        //situation = new SituationTravelling(this);
        //UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.InitLocationPanel(curLocation);
        //UpdateLog($"Travelling trough {curLocation.name}");
        Debug.Log($"{hero.name} triggered {AnimationTrigger.HeroTravelling.ToString()}");
        // start travelling animation
        //expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.HeroTravelling.ToString());
    }

    public void InitEnemyEncounterSituation()
    {
        //situation = new CombatManager(this, curLocation.enemies);
        //var enemy = (situation as CombatManager).enemy;
        //UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.InitEnemyPanel(enemy);
        //UpdateLog($"{hero.name} started combat with {enemy.enemyData.name}");
        Debug.Log($"{hero.name} triggered {AnimationTrigger.BeginEncounter.ToString()}");
        //expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        //expPreviewPanel.eventAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        //expPreviewPanel.interAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        //// hide "dead" status icon
        //expPreviewPanel.enemyStatusIcon.enabled = false;
    }
}