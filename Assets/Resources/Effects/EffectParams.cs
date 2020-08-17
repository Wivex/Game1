using System;
using System.Collections.Generic;
using UnityEngine;

//public enum EffectType
//{
//    Damage,
//    Healing,
//    EnergyGain,
//    EnergyLoss,
//    StatModifier
//}

public enum ProcType
{
    Instant,
    Duration,
    Delayed,
    DelayedAndDuration
}

[Serializable]
public class EffectParams
{
    public EffectType type;
    public ProcType procType;
    public TargetType target;
    [HideIfNotEnumValues("procType", ProcType.Duration, ProcType.DelayedAndDuration)]
    public int duration;
    [HideIfNotEnumValues("procType", ProcType.Delayed, ProcType.DelayedAndDuration)]
    public int delay;
    [HideIfNotStringValues("effectName", "Damage")]
    public DamageType damageType;
    [HideIfNotStringValues("effectName", "StatModifier")]
    public StatType stat;
    [HideIfNotStringValues("effectName", "StatModifier")]
    public StatModType statModType;
    public int amount;

    [HideInInspector]
    public string effectName;
}