using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();
    internal List<Tactic> tactics;

    internal Dictionary<StatType, Stat> stats;

    internal bool Dead => HP <= 0;

    #region STATS SHORTCUTS

    internal int HP
    {
        get => (stats[StatType.Health] as StatDepletable).CurValue;
        set => (stats[StatType.Health] as StatDepletable).CurValue = value;
    }
    internal int HPMax => stats[StatType.Health].ModdedValue;
    internal int Energy
    {
        get => (stats[StatType.Energy] as StatDepletable).CurValue;
        set => (stats[StatType.Energy] as StatDepletable).CurValue = value;
    }
    internal int EnergyMax => stats[StatType.Energy].ModdedValue;
    internal int Speed => stats[StatType.Speed].ModdedValue;
    internal int Attack => stats[StatType.Attack].ModdedValue;
    internal int Defence => stats[StatType.Defence].ModdedValue;

    #endregion

    internal void InitData(UnitData data)
    {
        stats = new Dictionary<StatType, Stat>
        {
            {StatType.Health, new StatDepletable(data.stats.health)},
            {StatType.Energy, new StatDepletable(data.stats.energy)},
            {StatType.Speed, new Stat(data.stats.speed)},
            {StatType.Attack, new Stat(data.stats.attack)},
            {StatType.Defence, new Stat(data.stats.defence)}
        };

        foreach (var abilityData in data.abilities)
            abilities.Add(new Ability(abilityData));

        tactics = data.tactics;
    }


    #region OPERATIONS

    // NOTE: keep everything here, what can happen put of combat (events) 
    public virtual int TakeDamage(Expedition exp, Damage damage)
    {
        var protectionValue = 0;
        switch (damage.type)
        {
            case DamageType.Physical:
                protectionValue = stats[StatType.Defence].ModdedValue;
                break;
        }

        var healthLoss = Math.Max(damage.amount - protectionValue, 0);
        HP = Math.Max(HP - healthLoss, 0);

        // invoked here, cause can take damage outside of combat
        UIManager.i.CreateFloatingTextForUnit(exp, this, -healthLoss);

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