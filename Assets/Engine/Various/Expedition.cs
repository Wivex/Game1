using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AnimTriggerType
{
    HeroTravelling,
    HeroIdle,
    HeroEncounter
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
        this.destination = destination;
        curLocation = Resources.Load<LocationData>("Locations/Forest/Forest");
        ResetTimers();
    }

    public void ResetTimers()
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
            ResetTimers();
            return true;
        }
        return false;
    }

    public void UpdateSituations()
    {
        if (situation == null)
            InitTravellingSituation();
        else if (GraceTimePassed())
            TryNewSituation();
        else
            situation.Update();
    }

    public void TryNewSituation()
    {
        Debug.Log("Tried new Sit");
        foreach (var sit in curLocation.situations)
        {
            if (Random.value < sit.chance)
            {
                switch (sit.SituationType)
                {
                    case SituationType.Travelling:
                        InitTravellingSituation();
                        break;
                    case SituationType.EnemyEncounter:
                        InitEnemyEncounterSituation();
                        break;
                    case SituationType.ObjectEncounter:
                        //situation = new SituationCombat(location.enemies);
                        //expPanel.detailsPanelDrawer.enemyPanel.gameObject.SetActive(false);
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
        // start travelling animation
        expPreviewPanel.heroAnim.SetTrigger(AnimTriggerType.HeroTravelling.ToString());
        Debug.Log("Travelling");
    }

    public void InitEnemyEncounterSituation()
    {
        situation = new SituationCombat(this, curLocation.enemies);
        var enemy = (situation as SituationCombat).enemy;
        UIManager.instance.expPanelDrawer.detailsPanelDrawer.InitEnemyPanel(enemy);
        UpdateLog($"{hero.name} started combat with {enemy.enemyData.name}");
        expPreviewPanel.heroAnim.SetTrigger(AnimTriggerType.HeroEncounter.ToString());
        expPreviewPanel.eventAnim.SetTrigger(AnimTriggerType.HeroEncounter.ToString());
        Debug.Log("Encounter");
    }
}