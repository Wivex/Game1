using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class UnitStatsManager
{
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



    //public int TakeDamage(Damage damage)
    //{
    //    var protectionValue = 0;
    //    switch (damage.type)
    //    {
    //        case DamageType.Physical:
    //            protectionValue = baseStats[(int) StatType.Defence].curValue;
    //            break;
    //        case DamageType.Hazardous:
    //            protectionValue = baseStats[(int) StatType.HResist].curValue;
    //            break;
    //        case DamageType.Bleeding:
    //            protectionValue = baseStats[(int) StatType.BResist].curValue;
    //            break;
    //    }

    //    var healthLoss = Math.Max(damage.amount - protectionValue, 0);
    //    baseStats[(int) StatType.Health].curValue = Math.Max(baseStats[(int) StatType.Health].curValue - healthLoss, 0);

    //    CreateFloatingText(unitDetailsIcon, -healthLoss);
    //    CreateFloatingText(unitPreviewIcon, -healthLoss);

    //    return healthLoss;
    //}

    //public void Heal(int amount)
    //{
    //    baseStats[(int) StatType.Health].curValue = Mathf.Min(baseStats[(int) StatType.Health].curValue + amount,
    //        (baseStats[(int) StatType.Health] as StatChanging).maxValue);

    //    CreateFloatingText(unitDetailsIcon, amount);
    //    CreateFloatingText(unitPreviewIcon, amount);
    //}

    public static void InitStats(this Unit unit, UnitData data)
    {
        unit.curStats.health = data.stats.health;
        unit.curStats.energy = data.stats.energy;
        unit.curStats.attack = data.stats.attack;
        unit.curStats.defence = data.stats.defence;
        unit.curStats.speed = data.stats.speed;
    }
}