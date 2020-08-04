using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnergyType
{
    None,
    Mana,
    Stamina,
    Fury
}

public enum StatType
{
    Health,
    Energy,
    Attack,
    Defence,
    Speed
}

/// <summary>
/// Simple stat values representation for unity display convenience
/// </summary>
[Serializable]
public class StatsSheet
{
    public int health,
        energy,
        attack,
        defence,
        speed;
}