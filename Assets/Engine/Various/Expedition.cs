using UnityEngine;

public class Expedition
{
    public Hero hero;
    public Situation situation;
    public LocationData curLocation;
    public LocationType destination;
    public ExpeditionPanelDrawer expeditionPanel;

    public Expedition(ExpeditionPanelDrawer expeditionPanel, Hero hero, LocationType destination)
    {
        this.hero = hero;
        this.destination = destination;
        curLocation = Resources.Load<LocationData>("Locations/Forest");
        this.expeditionPanel = expeditionPanel;
        expeditionPanel.expedition = this;
    }

    public void UpdateLog(string logEntry)
    {
        expeditionPanel.logPanel.AddLogEntry(logEntry);
    }

    public void UpdateSituations()
    {
        if (situation == null) situation = new SituationTravelling(this);
        if (situation.readyForNewSituation)
            TryNewSituation();
        else
            situation.Update();
    }

    public void TryNewSituation()
    {
        foreach (var sit in curLocation.situations)
        {
            if (Random.value < sit.chance)
                switch (sit.SituationType)
                {
                    case SituationType.Travelling:
                        situation = new SituationTravelling(this);
                        expeditionPanel.situationPanel.InitLocationPanel(curLocation);
                        UpdateLog($"Travelling trough {curLocation.name}");
                        break;
                    case SituationType.EnemyEncounter:
                        situation = new SituationCombat(this, curLocation.enemies);
                        var enemy = (situation as SituationCombat).enemy;
                        expeditionPanel.situationPanel.InitEnemyPanel(enemy);
                        UpdateLog($"{hero.name} started combat with {enemy.enemyData.name}");
                        break;
                    case SituationType.POIEncounter:
                        //situation = new SituationCombat(location.enemies);
                        //expeditionPanel.situationPanel.enemyPanel.gameObject.SetActive(false);
                        break;
                }
        }
    }
}