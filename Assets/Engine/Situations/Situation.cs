using UnityEngine;

public enum SituationType
{
    Travelling,
    EnemyEncounter,
    ObjectEncounter
}

public abstract class Situation
{
    public Expedition expedition;
    public SituationType type;
    public RectTransform expSituationPanel;
    public bool resolved, animPrepared;

    protected Situation(Expedition expedition)
    {
        this.expedition = expedition;
    }

    public abstract void Update();
}