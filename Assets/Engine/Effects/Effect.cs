using System;
using UnityEngine;

public enum EffectType
{
    StatDirectChange,
    StatModifier
}

public enum DurationType
{
    Instant,
    Persistent,
    Repeating
}

[Serializable]
public class Effect
{
    [HideInInspector]
    public string name;
    [HideInInspector]
    public Sprite icon;
    [HideInInspector]
    public int curDuration;

    public Target target;
    public EffectType effectType;
    [ShownIfEnumValue("effectType", (int)EffectType.StatDirectChange)]
    public DamageType damageType;
    [ShownIfEnumValue("effectType", (int)EffectType.StatModifier)]
    public StatModType statModType;
    public DurationType durationType;
    [ShownIfEnumValue("durationType", (int)DurationType.Persistent, (int)DurationType.Repeating)]
    public int duration;
    public StatType stat;
    public int value;

    public Effect(AbilityData abilityData, int duration)
    {
        name = abilityData.name;
        icon = abilityData.icon;
        curDuration = duration;
    }

    public void ApplyEffect(SituationCombat situation)
    {
        switch (target)
        {
            case Target.Self:
                situation.actor.AddEffect(this);
                break;
            case Target.Foe:
                situation.target.AddEffect(this);
                break;
        }
    }

    public void UpdateEffect(Unit unit)
    {
        curDuration--;
        switch (durationType)
        {
            case DurationType.Persistent:
                if (value > 0)
                    DamageTarget(situation);
                if (value < 0)
                    switch (target)
                    {
                        case Target.Hero:
                            situation.hero.Heal(value);
                            break;
                        case Target.Enemy:
                            situation.enemy.Heal(value);
                            break;
                    }
                break;
            case DurationType.Repeating:
                ApplyStatModifier(situation);
                break;
        }
    }



    #region EFFECTS
    void AffectHealth(SituationCombat situation)
    {
        switch (durationType)
        {
            case DurationType.Instant:
                if (value > 0)
                    DamageTarget(situation);
                if (value < 0)
                    switch (target)
                    {
                        case Target.Self:
                            situation.actor.Heal(value);
                            break;
                        case Target.Foe:
                            situation.target.Heal(value);
                            break;
                    }
                break;
            case DurationType.Persistent:
                situation.hero.curEffects.Add(this);
                break;
            case DurationType.Repeating:
                ApplyStatModifier(situation);
                break;
        }
    }

    void ApplyStatModifier(SituationCombat situation)
    {
        switch (target)
        {
            case Target.Self:
                situation.actor.stats[(int)stat]
                    .AddModifier(new StatModifier(value, statModType));
                situation.hero.curEffects.Add(this);
                break;
            case Target.Foe:
                situation.target.stats[(int)stat]
                    .AddModifier(new StatModifier(value, statModType));
                situation.enemy.curEffects.Add(this);
                break;
        }
    }

    void DamageTarget(SituationCombat situation)
    {
        switch (target)
        {
            case Target.Self:
                situation.actor.TakeDamage(new Damage(damageType, value,
                    situation.hero));
                break;
            case Target.Foe:
                situation.target.TakeDamage(new Damage(damageType, value,
                    situation.enemy));
                break;
        }
    }
    #endregion
}