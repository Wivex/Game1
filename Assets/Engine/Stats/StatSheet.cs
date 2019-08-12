using System;
using System.Collections.Generic;
using UnityEngine;

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

[Serializable]
public struct StatSheet
{
    public Stat health,
        energy,
        attack,
        defence,
        speed,
        hResist,
        bResist;
}