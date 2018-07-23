using UnityEngine;

public enum SituationType
{
    Travelling,
    EnemyEncounter,
    POIEncounter
}

public abstract class Situation
{
    public Expedition expedition;
    public SituationType type;
    public RectTransform expSituationPanel;
    public bool readyForNewSituation;

    protected Situation(Expedition expedition)
    {
        this.expedition = expedition;
    }

    public abstract void Update();
}