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

    public void ApplyEffect(SituationCombat situation, Unit unit, AbilityData abilityData)
    {
        name = abilityData.name;
        icon = abilityData.icon;
        curDuration = duration;
        curDelay = delay;

        if (effectApplyType != EffectApplyType.Delayed) ProcEffect(situation, unit);
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
                    LogEvent(situation, $"{unit.name} got {amount} change in {stat} from {name} effect.");
                }
                else
                    unit.curEffects.Add(this);
                break;
        }
    }

    public void UpdateEffect(SituationCombat situation, Unit unit)
    {
        if (effectApplyType == EffectApplyType.Delayed && curDelay-- > 0) return;

        ProcEffect(situation, unit);
        if (--curDuration <= 0) RemoveEffect(unit);
    }

    public void RemoveEffect(Unit unit)
    {
        if (effectOnStatsType == EffectOnStatsType.StatModifier)
            unit.stats[(int) stat].RemoveAllModsFromSource(this);

        unit.curEffects.Remove(this);
    }

    public void LogEvent(SituationCombat situation, string text)
    {
        situation.expedition.expeditionPanel.UpdateLog(text);
    }
}