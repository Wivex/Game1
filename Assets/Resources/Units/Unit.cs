using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Unit
{
    internal List<Ability> abilities = new List<Ability>();
    internal UnitEffects unitEffects = new UnitEffects();
    internal Dictionary<StatType, Stat> baseStats;
    internal List<Tactic> tactics;
    
    internal event Action<Unit> CooldownsUpdated;
    internal event Action<Unit, Damage> TookDamage;
    internal event Action<Unit, EffectOverTimeType> EffectAdded, EffectApplied, EffectRemoved;

    internal bool Dead => HP <= 0;
    internal abstract string Name { get; }

    #region STATS SHORTCUTS

    internal int HP
    {
        get => (baseStats[StatType.Health] as StatDepletable).CurValue;
        set => (baseStats[StatType.Health] as StatDepletable).CurValue = value;
    }

    internal int Energy
    {
        get => (baseStats[StatType.Energy] as StatDepletable).CurValue;
        set => (baseStats[StatType.Energy] as StatDepletable).CurValue = value;
    }

    internal int AP { get; set; }

    internal int HPMax => baseStats[StatType.Health].ModdedValue;
    internal int EnergyMax => baseStats[StatType.Energy].ModdedValue;
    internal int APMax => baseStats[StatType.Speed].ModdedValue * Combat.AP_AccumulationLimitMod;

    internal int Speed => baseStats[StatType.Speed].ModdedValue;
    internal int Attack => baseStats[StatType.Attack].ModdedValue;
    internal int Defence => baseStats[StatType.Defence].ModdedValue;

    #endregion

    protected Unit(UnitData data)
    {
        baseStats = new Dictionary<StatType, Stat>
        {
            {StatType.Health, new StatDepletable(data.stats.health)},
            {StatType.Energy, new StatDepletable(data.stats.energy)},
            {StatType.Speed, new Stat(data.stats.speed)},
            {StatType.Attack, new Stat(data.stats.attack)},
            {StatType.Defence, new Stat(data.stats.defence)}
        };

        foreach (var abilityData in data.abilities)
            abilities.Add(new Ability(abilityData));
    }

    internal void UpdateCooldowns()
    {
        abilities.ForEach(abil => abil.UpdateCooldown());
        CooldownsUpdated?.Invoke(this);
    }

    internal void ApplyDamage(Damage damage)
    {
        var protectionValue = 0;
        switch (damage.type)
        {
            case DamageType.Physical:
                protectionValue = baseStats[StatType.Defence].ModdedValue;
                break;
        }
        var damAfterDR = Math.Max(damage.amount - protectionValue, 0);
        damage.amount = damAfterDR;
        HP = Math.Max(HP - damAfterDR, 0);

        TookDamage?.Invoke(this, damage);
    }
    
    internal void AddEffect(EffectOverTimeData effectData)
    {
        unitEffects.Add(new EffectOverTime(effectData));
        EffectAdded?.Invoke(this, effectData.type);
    }
    
    internal void ApplyNextEffectType()
    {
        var appliedType = unitEffects.ApplyNextEffectType(this);
        EffectApplied?.Invoke(this, appliedType);
    }
    
    internal void RemoveEffect(EffectOverTime effect)
    {
        unitEffects.Remove(effect);
        EffectRemoved?.Invoke(this, effect.data.type);
    }
}