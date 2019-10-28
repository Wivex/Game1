using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    /// <summary>
    /// Base stats of unit, affected by persistent modifiers (gear, special persistent effects). Considered maximums for current stats.
    /// </summary>
    internal DataStats baseStats = new DataStats();
    /// <summary>
    /// Current stats of unit, affected by temporary effects or damage
    /// </summary>
    internal DataStats curStats = new DataStats();
    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();
    internal List<Tactic> tactics;
    
    internal Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>
    {
        {StatType.Health, new Stat(100)},
        {StatType.Attack, new Stat(10)},
        {StatType.Speed, new Stat(10)},
    };

    internal bool Dead => curStats.health <= 0;

    internal void InitData(UnitData data)
    {
        baseStats.health = data.baseStats.health;
        baseStats.energy = data.baseStats.energy;
        baseStats.attack = data.baseStats.attack;
        baseStats.defence = data.baseStats.defence;
        baseStats.speed = data.baseStats.speed;

        curStats.health = data.baseStats.health;
        curStats.energy = data.baseStats.energy;
        curStats.attack = data.baseStats.attack;
        curStats.defence = data.baseStats.defence;
        curStats.speed = data.baseStats.speed;

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
                protectionValue = curStats.defence;
                break;
            case DamageType.Elemental:
                protectionValue = curStats.eResist;
                break;
            case DamageType.Bleeding:
                protectionValue = curStats.bResist;
                break;
        }

        var healthLoss = Math.Max(damage.amount - protectionValue, 0);
        curStats.health = Math.Max(curStats.health - healthLoss, 0);

        // invoked here, cause can take damage outside of combat
        UIManager.i.CreateFloatingTextForUnit(exp, this, -healthLoss);

        return healthLoss;
    }

    public virtual int Heal(int amount, params Transform[] UItargets)
    {
        var value = Mathf.Min(curStats.health + amount, baseStats.health);
        curStats.health = value;

        //UItargets.ForEach(UIelem => UIManager.i.CreateFloatingText(UIelem, value));

        return value;
    }

    public virtual void Kill()
    {
        // TODO: implement
    }

    #endregion
}