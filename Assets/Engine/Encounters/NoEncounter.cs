using UnityEngine;
using UnityEditor;

public enum EncounterType
{
    None,
    Enemy,
    EnemyCamp,
    Container,
    Resources,
    Intel,
    NPC
}

public class NoEncounter
{
    internal Mission mis;
    internal Hero hero;
    internal EncounterType type = EncounterType.None;

    internal NoEncounter(Mission mis)
    {
        this.mis = mis;
        hero = mis.hero;
    }

    /// <summary>
    /// Runs next logical action in the encounter, when all previous action animations are finished.
    /// </summary>
    internal virtual void NextAction(){}
}