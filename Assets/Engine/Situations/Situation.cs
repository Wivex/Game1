using UnityEngine;

public enum SituationType
{
    Travelling,
    EnemyEncounter,
    ObjectEncounter
}

public enum SituationState
{
    Preparing,
    Updating,
    Animating,
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
        state = SituationState.Preparing;
    }

    public void Resolve()
    {
        state = SituationState.Resolved;
        expedition.ResetGraceTimers();
    }

    public abstract void Update();
}