using System;
using System.Collections.Generic;
using UnityEngine;

public enum EffectDirectType
{
    Damage,
    Heal,
    EnergyGain,
    EnergyLoss
}

[Serializable]
public class EffectDirectData
{
    public EffectDirectType type;
    public DamageType damageType;
    public TargetType target;
    public int amount;
}