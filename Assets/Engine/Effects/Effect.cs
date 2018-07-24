using System;
using UnityEngine;

public enum EffectOnStatsType
{
    Damage,
    Heal,
    StatModifier
}

public enum EffectApplyType
{
    Instant,
    Continueous,
    Delayed
}

[Serializable]
public class Effect
{
    [HideInInspector] public string name;
    [HideInInspector] public Sprite icon;
    [HideInInspector] public int curDuration, curDelay;

    public Target target;
    public EffectApplyType effectApplyType;
    [ShownIfEnumValue("effectApplyType", (int)EffectApplyType.Continueous, (int)EffectApplyType.Delayed)]
    public int duration;
    [ShownIfEnumValue("effectApplyType", (int)EffectApplyType.Delayed)]
    public int delay;
    public EffectOnStatsType effectOnStatsType;
    [ShownIfEnumValue("effectOnStatsType", (int)EffectOnStatsType.Damage)]
    public DamageType damageType;
    [ShownIfEnumValue("effectOnStatsType", (int)EffectOnStatsType.StatModifier)]
    public StatType stat;
    [ShownIfEnumValue("effectOnStatsType", (int)EffectOnStatsType.StatModifier)]
    public StatModType statModType;

    public int amount;

    public Effect(AbilityData abilityData)
    {
        name = abilityData.name;
        icon = abilityData.icon;
        curDuration = duration;
        curDelay = delay;
    }

    public void ApplyEffect(Unit unit)
    {
        if (effectApplyType != EffectApplyType.Delayed)
            ProcEffect(unit);
        if (effectApplyType != EffectApplyType.Instant)
            unit.curEffects.Add(this);
    }

    public void ProcEffect(Unit unit)
    {
        switch (effectOnStatsType)
        {
            case EffectOnStatsType.Damage:
                unit.TakeDamage(new Damage(damageType, amount));
                break;
            case EffectOnStatsType.Heal:
                unit.Heal(amount);
                break;
            case EffectOnStatsType.StatModifier:
                // add modifier only once
                if (!unit.curEffects.Contains(this))
                    unit.stats[(int)stat].AddModifier(new StatModifier(amount, statModType, this));
                break;
        }
    }

    public void UpdateEffect(Unit unit)
    {
        if (effectApplyType == EffectApplyType.Delayed)
        {
            curDelay++;
        }
        else
        {
            ProcEffect(unit);
            if (--curDuration < 0) RemoveEffect(unit);
        }
    }

    public void RemoveEffect(Unit unit)
    {
        if (effectOnStatsType == EffectOnStatsType.StatModifier)
            unit.stats[(int) stat].RemoveAllModsFromSource(this);

        unit.curEffects.Remove(this);
    }
}