using UnityEngine;
using UnityEditor;

public enum EncounterType
{
    Enemy,
    Container,
    PlaceOfPower,
    EnemyCamp,
    Resources,
    Intel,
    NPC
}

public abstract class Encounter
{
    internal Mission mis;
    internal EncounterType type;

    /// <summary>
    /// Runs next logical action in the encounter, when all previous action animations are finished.
    /// </summary>
    internal abstract void NextStage();

    internal Encounter(Mission mis)
    {
        this.mis = mis;
    }
}