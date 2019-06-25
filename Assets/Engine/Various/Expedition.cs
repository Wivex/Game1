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
    public Hero hero;
    public Situation situation;
    public LocationData curLocation;
    public LocationType destination;

    public ExpPreviewPanelDrawer expPreviewPanel;

    DateTime lastSituationRealTime;
    float lastSituationGameTime;

    public Expedition(Hero hero, LocationType destination)
    {
        this.hero = hero;
        UIManager.instance.expPanelDrawer.NewPreviewPanel(this);
        hero.unitPreviewIcon = expPreviewPanel.heroIcon.transform;
        hero.unitDetailsIcon = UIManager.instance.expPanelDrawer.detailsPanelDrawer.heroPanel.unitImage.transform;
        this.destination = destination;
        curLocation = Resources.Load<LocationData>("Locations/Forest/Forest");
        ResetGraceTimers();
        InitTravellingSituation();
    }

    public bool GraceTimePassed =>
        (DateTime.Now - lastSituationRealTime).TotalSeconds > GameManager.instance.minGracePeriod &&
        Time.time - lastSituationGameTime > GameManager.instance.minGracePeriod;

    public void UpdateSituation()
    {
        if (situation.state == SituationState.RunningLogic)
            situation.Update();
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
        situation.state = SituationState.RunningLogic;
    }

    public void TryNewSituation()
    {
        if (GraceTimePassed)
        {
            foreach (var sit in curLocation.situations)
            {
                if (Random.value < sit.chance)
                {
                    switch (sit.SituationType)
                    {
                        case SituationType.EnemyEncounter:
                            InitEnemyEncounterSituation();
                            break;
                        case SituationType.ObjectEncounter:
                            //situation = new SituationCombat(location.enemies);
                            //expPanel.detailsPanelDrawer.enemyPanel.gameObject.SetActive(false);
                            break;
                    }

                    // if any situation occured, exit sequence
                    return;
                }
            }
        }
        // too early for new situation, travel for a bit
        else
        {
            // if not already travelling
            if (situation.type != SituationType.Travelling)
                InitTravellingSituation();
        }
    }

    public void InitTravellingSituation()
    {
        situation = new SituationTravelling(this);
        UIManager.instance.expPanelDrawer.detailsPanelDrawer.InitLocationPanel(curLocation);
        UpdateLog($"Travelling trough {curLocation.name}");
        //Debug.Log($"{hero.name} triggered {AnimationTrigger.HeroTravelling.ToString()}");
        // start travelling animation
        expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.HeroTravelling.ToString());
    }

    public void InitEnemyEncounterSituation()
    {
        situation = new SituationCombat(this, curLocation.enemies);
        var enemy = (situation as SituationCombat).enemy;
        UIManager.instance.expPanelDrawer.detailsPanelDrawer.InitEnemyPanel(enemy);
        UpdateLog($"{hero.name} started combat with {enemy.enemyData.name}");
        Debug.Log($"{hero.name} triggered {AnimationTrigger.BeginEncounter.ToString()}");
        expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        expPreviewPanel.eventAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
        expPreviewPanel.interAnim.SetTrigger(AnimationTrigger.BeginEncounter.ToString());
    }
}