using UnityEngine;

public enum SituationType
{
    Travelling,
    EnemyEncounter,
    ObjectEncounter
}

public enum SituationState
{
    Preparation,
    Progressing,
    BusyAnimating,
    Resolved
}

public abstract class Situation
{
    public Expedition expedition;
    public SituationType type;
    public RectTransform expSituationPanel;

    internal SituationState state;

    protected Situation(Expedition expedition)
    {
        this.expedition = expedition;
        state = SituationState.Preparation;
    }

    public abstract void Update();
}