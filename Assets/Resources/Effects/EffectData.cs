using System;
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
public class EffectData
{
    public ProcType procType;
    public EffectType effectType;
    public TargetType target;
    // NOTE: check >2
    [Min(2), HideIfNotEnumValues("procType", ProcType.Duration, ProcType.DelayedAndDuration)]
    public int duration;
    [HideIfNotEnumValues("procType", ProcType.Delayed, ProcType.DelayedAndDuration)]
    public int delay;
    [HideIfNotEnumValues("effectType", EffectType.Damage)]
    public DamageType damageType;
    [HideIfNotEnumValues("effectType", EffectType.StatModifier)]
    public StatType stat;
    [HideIfNotEnumValues("effectType", EffectType.StatModifier)]
    public StatModType statModType;
    public int amount;
    public GameObject procAnimation;

    internal string name;
    internal Sprite icon;
    internal int curDuration;
    internal Combat combat;
    internal Unit targetUnit;
}