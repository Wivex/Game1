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
    Continuous,
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
    [ShownIfEnumValue("effectApplyType", (int)EffectApplyType.Continuous, (int)EffectApplyType.Delayed)]
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

    public void ApplyEffect(SituationCombat situation, Unit unit, AbilityData abilityData)
    {
        name = abilityData.name;
        icon = abilityData.icon;
        curDuration = duration;
        curDelay = delay;

        if (effectApplyType == EffectApplyType.Instant ||
            effectOnStatsType == EffectOnStatsType.StatModifier) ProcEffect(situation, unit);
        if (effectApplyType != EffectApplyType.Instant) unit.curEffects.Add(this);
    }

    public void ProcEffect(SituationCombat situation, Unit unit)
    {
        switch (effectOnStatsType)
        {
            case EffectOnStatsType.Damage:
                var dam = unit.TakeDamage(new Damage(damageType, amount));
                LogEvent(situation, $"{unit.name} suffered from {name} effect for {dam} {damageType} damage.");
                break;
            case EffectOnStatsType.Heal:
                unit.Heal(amount);
                LogEvent(situation, $"{unit.name} healed from {name} effect for {amount} health.");
                break;
            case EffectOnStatsType.StatModifier:
                // add modifier only once
                if (!unit.curEffects.Contains(this))
                {
                    unit.stats[(int) stat].AddModifier(new StatModifier(amount, statModType, this));
                    LogEvent(situation, $"{unit.name} got {ColoredValue(amount)} {stat} from {name} for {duration} turns.");
                }
                break;
        }
    }

    public void UpdateEffect(SituationCombat situation, Unit unit)
    {
        if (effectApplyType == EffectApplyType.Delayed && curDelay-- > 0)
            return;

        if (curDuration-- > 0) ProcEffect(situation, unit);

        if (curDuration <= 0)
            RemoveEffect(unit);
    }

    public void RemoveEffect(Unit unit)
    {
        if (effectOnStatsType == EffectOnStatsType.StatModifier)
            unit.stats[(int) stat].RemoveAllModsFromSource(this);

        unit.curEffects.Remove(this);
    }

    public void LogEvent(SituationCombat situation, string text)
    {
        situation.expedition.UpdateLog(text);
    }

    string ColoredValue(int value)
    {
        if (value < 0) return $"<color=\"red\">-{value}</color>";
        if (value > 0) return $"<color=\"green\">+{value}</color>";
        return $"{value}";
    }
}