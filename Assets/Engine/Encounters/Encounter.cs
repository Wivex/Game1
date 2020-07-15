﻿using UnityEngine;
using UnityEditor;

public enum EncounterType
{
    None,
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
    internal abstract void NextAction();

    internal Encounter(Mission mis)
    {
        this.mis = mis;
    }
}