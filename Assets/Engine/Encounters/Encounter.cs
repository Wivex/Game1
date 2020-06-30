using UnityEngine;
using UnityEditor;

public enum EncounterType
{
    None,
    Combat,
    POI,
    Container
}

public enum EncounterState
{
    InProgress,
    Finished
}

public abstract class Encounter
{
    internal Expedition exp;
    internal EncounterState state;
    internal EncounterType type;

    internal abstract void Update();

    internal virtual void InitEncounter(Expedition exp)
    {
        this.exp = exp;
        state = EncounterState.InProgress;
    }
}