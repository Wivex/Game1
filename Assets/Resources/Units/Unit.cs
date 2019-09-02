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
    internal UnitStats baseStats;
    /// <summary>
    /// Current stats of unit, affected by temporary effects or damage
    /// </summary>
    internal UnitStats curStats = new UnitStats();
    internal float initiative;
    internal TacticsPreset tacticsPreset;

    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();

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

    ///// <summary>
    ///// Base value from unit stats
    ///// </summary>
    //public int BaseValuea => baseValue;
    ///// <summary>
    ///// Persistent modified value, affected by gear and other long-term effects
    ///// </summary>
    //public int PersModValue => baseValue;
    ///// <summary>
    ///// Current value, persistent modified value affected by temporary effects
    ///// </summary>
    //public int CurValue => baseValue;
    ///// <summary>
    ///// Maximum current value, special case usage for Health and Energy
    ///// </summary>
    //public int MaxCurValue => baseValue;

    public int TakeDamage(Damage damage, params Transform[] UItargets)
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

    public void Heal(int amount)
    {
        curStats.health = Mathf.Min(curStats.health + amount,
            baseStats.health);

        //UNDONE
        //CreateFloatingText(unitDetailsIcon, amount);
        //CreateFloatingText(unitPreviewIcon, amount);
    }
}