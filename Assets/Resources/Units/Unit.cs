using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Unit
{
    /// <summary>
    /// Base stats of unit, affected by persistent modifiers (gear, special persistent effects). Considered maximums for current stats.
    /// </summary>
    internal UnitStats baseStats = new UnitStats();
    /// <summary>
    /// Current stats of unit, affected by temporary effects or damage
    /// </summary>
    internal UnitStats curStats = new UnitStats();
    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();
    internal TacticsPreset tacticsPreset;

    internal bool Dead => curStats.health <= 0;

    internal void InitData(UnitData data)
    {
        curStats.health = data.stats.health;
        curStats.energy = data.stats.energy;
        curStats.attack = data.stats.attack;
        curStats.defence = data.stats.defence;
        curStats.speed = data.stats.speed;

        foreach (var abilityData in data.abilities)
            abilities.Add(new Ability(abilityData));

        tacticsPreset = data.tacticsPreset;
    }


    #region OPERATIONS

    // NOTE: keep everything here, what can happen put of combat (events) 
    public virtual int TakeDamage(Damage damage, params Transform[] UItargets)
    {
        var protectionValue = 0;
        switch (damage.type)
        {
            case DamageType.Physical:
                protectionValue = curStats.defence;
                break;
            case DamageType.Hazardous:
                protectionValue = curStats.hResist;
                break;
            case DamageType.Bleeding:
                protectionValue = curStats.bResist;
                break;
        }

        var healthLoss = Math.Max(damage.amount - protectionValue, 0);
        curStats.health = Math.Max(curStats.health - healthLoss, 0);

        UItargets.ForEach(UIelem => UIManager.i.CreateFloatingText(UIelem, -healthLoss));

        return healthLoss;
    }

    public virtual int Heal(int amount, params Transform[] UItargets)
    {
        var value = Mathf.Min(curStats.health + amount, baseStats.health);
        curStats.health = value;

        UItargets.ForEach(UIelem => UIManager.i.CreateFloatingText(UIelem, value));

        return value;
    }

    public virtual void Kill()
    {
        // TODO: implement
    }

    #endregion
}