using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Unit
{
    internal List<Ability> abilities = new List<Ability>();
    internal UnitEffectsStacks effects;
    internal Dictionary<StatType, Stat> baseStats;
    internal List<Tactic> tactics;
    internal int speedPoints;

    internal bool Dead => HP <= 0;
    internal abstract string Name { get; }

    #region STATS SHORTCUTS

    internal int HP
    {
        get => (baseStats[StatType.Health] as StatDepletable).CurValue;
        set => (baseStats[StatType.Health] as StatDepletable).CurValue = value;
    }
    internal int HPMax => baseStats[StatType.Health].ModdedValue;
    internal int Energy
    {
        get => (baseStats[StatType.Energy] as StatDepletable).CurValue;
        set => (baseStats[StatType.Energy] as StatDepletable).CurValue = value;
    }
    internal int EnergyMax => baseStats[StatType.Energy].ModdedValue;
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

        // NOTE: needed here?
        speedPoints = baseStats[StatType.Speed].ModdedValue;
    }

    internal void UpdateCooldowns()
    {
        abilities.ForEach(abil => abil.UpdateCooldown());
    }

    internal void UpdateEffects(Mission mis)
    {
        effectStacks.ForEach(stack => stack.NextTurn(mis, this));
    }
}