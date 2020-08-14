using System;
using UnityEngine;

[Serializable]
public abstract class EffectType
{
    public ProcType procType;
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