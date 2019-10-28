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
    internal  List<Tactic> tactics;

    internal bool Dead => curStats.health <= 0;

    internal void InitData(UnitData data)
    {
        baseStats.health = data.stats.health;
        baseStats.energy = data.stats.energy;
        baseStats.attack = data.stats.attack;
        baseStats.defence = data.stats.defence;
        baseStats.speed = data.stats.speed;

        curStats.health = data.stats.health;
        curStats.energy = data.stats.energy;
        curStats.attack = data.stats.attack;
        curStats.defence = data.stats.defence;
        curStats.speed = data.stats.speed;

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