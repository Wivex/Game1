using UnityEngine;
using UnityEditor;

public enum EncounterType
{
    None,
    Combat,
    POI
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

    internal Encounter(Expedition exp)
    {
        this.exp = exp;
        state = EncounterState.InProgress;
    }

    internal abstract void Update();
}