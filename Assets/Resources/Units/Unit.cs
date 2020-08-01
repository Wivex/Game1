using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();
    internal Dictionary<StatType, Stat> baseStats;

    internal int speedPoints;

    internal bool Dead => HP <= 0;

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

    public Unit(UnitData data)
    {
        baseStats = new Dictionary<StatType, Stat>
        {
            {StatType.Health, new StatDepletable(data.baseStats.health)},
            {StatType.Energy, new StatDepletable(data.baseStats.energy)},
            {StatType.Speed, new Stat(data.baseStats.speed)},
            {StatType.Attack, new Stat(data.baseStats.attack)},
            {StatType.Defence, new Stat(data.baseStats.defence)}
        };

        foreach (var abilityData in data.abilities)
            abilities.Add(new Ability(abilityData));

        speedPoints = baseStats[StatType.Speed].ModdedValue;
    }

    #region OPERATIONS

    // NOTE: keep everything here, what can happen put of enemyEncounter (events) 
    public virtual int TakeDamage(Mission exp, Damage damage)
    {
        var protectionValue = 0;
        switch (damage.type)
        {
            case DamageType.Physical:
                protectionValue = baseStats[StatType.Defence].ModdedValue;
                break;
        }

        var healthLoss = Math.Max(damage.amount - protectionValue, 0);
        HP = Math.Max(HP - healthLoss, 0);

        // invoked here, cause can take damage outside of enemyEncounter
        //UIManager.CreateFloatingTextForUnit(exp, this, -healthLoss);

        return healthLoss;
    }

    public virtual int Heal(int amount, params Transform[] UItargets)
    {
        var actualHeal = Mathf.Min(HP + amount, HPMax);
        HP += actualHeal;

        //UItargets.ForEach(UIelem => UIManager.i.CreateFloatingText(UIelem, value));

        return actualHeal;
    }

    public virtual void Kill()
    {
        // TODO: implement
    }

    #endregion
}