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
    [HideInInspector]
    public string name;
    [HideInInspector]
    public Sprite icon;
    [HideInInspector]
    public int curDuration, curDelay;

    public Target target;
    public EffectApplyType effectApplyType;
    //[ShownIfEnumValue("effectApplyType", (int) EffectApplyType.Continuous, (int) EffectApplyType.Delayed)]
    public int duration;
    //[ShownIfEnumValue("effectApplyType", (int) EffectApplyType.Delayed)]
    public int delay;
    public EffectOnStatsType effectOnStatsType;
    //[ShownIfEnumValue("effectOnStatsType", (int) EffectOnStatsType.Damage)]
    public DamageType damageType;
    //[ShownIfEnumValue("effectOnStatsType", (int) EffectOnStatsType.StatModifier)]
    public StatType stat;
    //[ShownIfEnumValue("effectOnStatsType", (int) EffectOnStatsType.StatModifier)]
    public StatModType statModType;

    public int amount;

    Unit targetUnit;
    CombatManager situation;

    public void ApplyEffect(CombatManager situation, Unit target, string effectName, Sprite effectIcon)
    {
        this.situation = situation;
        targetUnit = target;
        name = effectName;
        icon = effectIcon;
        curDuration = duration;
        curDelay = delay;

        if (effectApplyType == EffectApplyType.Instant ||
            effectOnStatsType == EffectOnStatsType.StatModifier) ProcEffect();
        if (effectApplyType != EffectApplyType.Instant) targetUnit.effects.Add(this);
    }

    void ProcEffect()
    {
        //switch (effectOnStatsType)
        //{
        //    case EffectOnStatsType.Damage:
        //        var dam = targetUnit.TakeDamage(new Damage(damageType, amount));
        //        AddEffectLogEntry(situation,
        //            $"{targetUnit.name} suffered from {name} effect for {dam} {damageType} damage.");
        //        break;
        //    case EffectOnStatsType.Heal:
        //        targetUnit.Heal(amount);
        //        AddEffectLogEntry(situation, $"{targetUnit.name} healed from {name} effect for {amount} health.");
        //        break;
        //    case EffectOnStatsType.StatModifier:
        //        // add modifier only once
        //        if (!targetUnit.effects.Contains(this))
        //        {
        //            targetUnit.baseStats[(int) stat].AddModifier(new StatModifier(amount, statModType, this));
        //            AddEffectLogEntry(situation,
        //                $"{targetUnit.name} got {ColoredValue(amount)} {stat} from {name} for {duration} turns.");
        //        }
        //        break;
        //}
    }

    public void UpdateEffect()
    {
        if (effectApplyType == EffectApplyType.Delayed && curDelay-- > 0)
            return;

        if (curDuration-- > 0) ProcEffect();

        if (curDuration <= 0)
            RemoveEffect(targetUnit);
    }

    public void RemoveEffect(Unit unit)
    {
        //if (effectOnStatsType == EffectOnStatsType.StatModifier)
        //    unit.baseStats[(int) stat].RemoveAllModsFromSource(this);

        unit.effects.Remove(this);
    }

    public void AddEffectLogEntry(CombatManager situation, string text)
    {
        //situation.expedition.UpdateLog(text);
    }

    string ColoredValue(int value)
    {
        if (value < 0) return $"<color=\"red\">-{value}</color>";
        if (value > 0) return $"<color=\"green\">+{value}</color>";
        return $"{value}";
    }
}