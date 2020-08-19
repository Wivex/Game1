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
public class EffectDirect
{
    public TargetType target;
    public EffectDirectType directEffect;
    public DamageType damageType;
    public int amount;
}