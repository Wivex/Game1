using UnityEngine;

public class Expedition
{
    public Hero hero;
    public Situation situation;
    public LocationData curLocation;
    public LocationType destination;

    public ExpeditionPanelDrawer expPanel;
    public ExpPreviewPanelDrawer expPreviewPanel;

    public Expedition(Hero hero, LocationType destination)
    {
        expPanel = UIManager.instance.expPanelDrawer;

        this.hero = hero;
        this.destination = destination;
        curLocation = Resources.Load<LocationData>("Locations/Forest/Forest");
        expPanel.selectedExp = this;
        expPanel.detailsPanelDrawer.InitHeroPanel(hero);
        situation = new SituationTravelling(this);
    }

    public void UpdateLog(string logEntry)
    {
        expPanel.logPanelDrawer.AddLogEntry(logEntry);
    }

    public void UpdateSituations()
    {
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
                        expPanel.detailsPanelDrawer.InitLocationPanel(curLocation);
                        UpdateLog($"Travelling trough {curLocation.name}");
                        break;
                    case SituationType.EnemyEncounter:
                        situation = new SituationCombat(this, curLocation.enemies);
                        var enemy = (situation as SituationCombat).enemy;
                        expPanel.detailsPanelDrawer.InitEnemyPanel(enemy);
                        UpdateLog($"{hero.name} started combat with {enemy.enemyData.name}");
                        break;
                    case SituationType.POIEncounter:
                        //situation = new SituationCombat(location.enemies);
                        //expPanel.detailsPanelDrawer.enemyPanel.gameObject.SetActive(false);
                        break;
                }
        }
    }
}