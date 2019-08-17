using System.Collections.Generic;
using UnityEngine;


public abstract class Unit
{
    [Header("Unit")]
    internal StatSheet curStats;
    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();

    internal void InitData(UnitData data)
    {
        //curStats.health = data.stats.health;
        //curStats.energy = data.stats.energy;
        //curStats.attack = data.stats.attack;
        //curStats.defence = data.stats.defence;
        //curStats.speed = data.stats.speed;

        foreach (var abilityData in data.abilities)
            abilities.Add(new Ability(abilityData));
    }

    // MOVE
    //void CreateFloatingText(Transform target, int value)
    //{
    //    var floatingText = Object.Instantiate(UIManager.instance.floatingTextPrefab, target);
    //    var textObject = floatingText.GetComponent<TextMeshProUGUI>();

    //    if (value > 0)
    //    {
    //        textObject.text = $"+{value}";
    //        textObject.color = Color.green;
    //    }

    //    if (value < 0)
    //    {
    //        textObject.text = $"{value}";
    //        textObject.color = Color.red;
    //    }
    //}

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
}