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
    float lastSituationInGameTimer;

    public Expedition(Hero hero, LocationType destination)
    {
        this.hero = hero;
        UIManager.instance.expPanelDrawer.NewPreviewPanel(this);
        hero.unitPreviewIcon = expPreviewPanel.heroIcon.transform;
        hero.unitDetailsIcon = UIManager.instance.expPanelDrawer.detailsPanelDrawer.heroPanel.unitImage.transform;
        this.destination = destination;
        curLocation = Resources.Load<LocationData>("Locations/Forest/Forest");
        InitTravellingSituation();
    }

    public void ResetGraceTimers()
    {
        lastSituationRealTime = DateTime.Now;
        lastSituationInGameTimer = GameManager.instance.minGracePeriod;
    }

    public void UpdateLog(string logEntry)
    {
        //expPanel.logPanelDrawer.AddLogEntry(logEntry);
    }

    // TODO: check if correct
    public bool GraceTimePassed()
    {
        lastSituationInGameTimer -= Time.deltaTime;
        if ((DateTime.Now - lastSituationRealTime).TotalSeconds > GameManager.instance.minGracePeriod &&
            lastSituationInGameTimer < 0)
        {
            ResetGraceTimers();
            return true;
        }

        return false;
    }

    public void UpdateSituation()
    {
        if (GraceTimePassed() && situation.state == SituationState.Resolved)
            TryNewSituation();
        else if (situation.state == SituationState.Updating)
            situation.Update();
    }

    public void TryNewSituation()
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
                    default:
                        if (situation.type != SituationType.Travelling)
                            InitTravellingSituation();
                        break;
                }

                // if any situation occured, break sequence
                return;
            }
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