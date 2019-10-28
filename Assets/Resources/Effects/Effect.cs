﻿using System;
using UnityEngine;

public enum EffectType
{
    Damage,
    Heal,
    StatModifier
}

[Serializable]
public class Effect
{
    public EffectType effectType;
    public Target target;
    public int duration;
    [HideIfNotEnumValues("effectType", EffectType.Damage)]
    public DamageType damageType;
    [HideIfNotEnumValues("effectType", EffectType.StatModifier)]
    public StatType stat;
    [HideIfNotEnumValues("effectType", EffectType.StatModifier)]
    public StatModType statModType;
    public int amount;

    internal string name;
    internal Sprite icon;
    internal int curDuration;
    
    protected Combat combat;
    protected Unit targetUnit;

    internal void AddEffect(Combat combat, string sourceName, Sprite sourceIcon)
    {
        this.combat = combat;
        name = sourceName;
        icon = sourceIcon;
        curDuration = duration;

        targetUnit = target == Target.Self ? combat.actor : combat.target;

        if (duration > 1)
        {
            targetUnit.effects.Add(this);
        }
        else
            ProcEffect();
    }

    protected virtual void ProcEffect()
    {
        if (curDuration-- <= 0)
            RemoveEffect(targetUnit);
    }

    public void RemoveEffect(Unit unit)
    {
        unit.effects.Remove(this);
    }

    public void AddEffectLogEntry(Combat combat, string text)
    {
        //situation.expedition.UpdateLog(text);


        // TODO: log coloring
        //string ColoredValue(int value)
        //{
        //    if (value < 0) return $"<color=\"red\">-{value}</color>";
        //    if (value > 0) return $"<color=\"green\">+{value}</color>";
        //    return $"{value}";
        //}
    }
}