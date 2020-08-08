using System;
using UnityEngine;

public enum EffectType
{
    Damage,
    Heal,
    StatModifier
}

[Serializable]
public class EffectData
{
    public EffectType effectType;
    public TargetType targetType;
    public int duration;
    public AnimationClip animation;
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

        targetUnit = targetType == TargetType.Hero ? combat.actor : combat.target;

        if (duration > 1)
        {
            targetUnit.effects.Add(this);
        }
        else
            ProcEffect();
    }

    internal virtual void ProcEffect()
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
        //situation.mission.UpdateLog(text);


        // TODO: log coloring
        //string ColoredValue(int value)
        //{
        //    if (value < 0) return $"<color=\"red\">-{value}</color>";
        //    if (value > 0) return $"<color=\"green\">+{value}</color>";
        //    return $"{value}";
        //}
    }
}