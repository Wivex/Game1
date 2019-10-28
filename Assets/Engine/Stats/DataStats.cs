using System;
using System.Collections.Generic;

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
    Speed,
    HResist,
    BResist
}

/// <summary>
/// Simple stat values representation for unity display convenience
/// </summary>
[Serializable]
public class DataStats
{
    public int health,
        energy,
        attack,
        defence,
        speed,
        eResist,
        bResist;
}

[Serializable]
public class UnitStatsNew
{
    public Stat health,
        energy,
        attack,
        defence,
        speed,
        eResist,
        bResist;
}