using System.Collections.Generic;
using UnityEngine;

public enum HeroState
{
    Recruit,
    Idle,
    OnMission
}

public enum ClassType
{
    Warrior
}

public enum SexType
{
    Male,
    Female
}

public class Hero : Unit
{
    internal string name; 
    internal SexType sex;
    internal HeroState state;
    internal HeroClassData data;
    internal int level = 0, gold = 0, experience = 0;
    internal Sprite portrait;
    internal Equipment equipment = new Equipment();
    internal List<Item> backpack = new List<Item>();
    internal List<Item> consumables = new List<Item>();

    internal Hero(string name, SexType sex, Sprite portrait, HeroClassData data) : base(data)
    {
        this.name = name;
        this.sex = sex;
        this.portrait = portrait;
        this.data = data;
    }

    // USE: TownManager.NewHero()
    // public Hero(string name = default,
    //               SexType sex = default,
    //               ClassType classType = default,
    //               Sprite portrait = default)
    // {
    //     this.sex = sex;
    //     this.heroClassType = classType;
    //     this.name = name ?? NamingManager.i.GetRandomHeroName(sex);
    //     this.portrait = portrait ?? RandomPortrait();
    //
    //     //HACK: temp solution
    //     data = c
    //     //HACK: temp solution
    //     state = HeroState.Recruit;
    //     //InitData(data);
    //     InitEquipment();
    // }

    // NOTE: non-reflection way to iterate equipment slots?
    void InitEquipment()
    {
        // equipment = data.equipment;

        TryEquipItem(equipment.head);
        TryEquipItem(equipment.body);
        TryEquipItem(equipment.boots);
        TryEquipItem(equipment.arms);
        TryEquipItem(equipment.amulet);
        TryEquipItem(equipment.belt);
        TryEquipItem(equipment.ring1);
        TryEquipItem(equipment.ring2);
        TryEquipItem(equipment.mainHand1);
        TryEquipItem(equipment.offHand1);
    }

    /// <summary>
    /// Add all non-zero stats as flat StatMod to the Hero stats
    /// </summary>
    void TryEquipItem(Item item)
    {
        if (item == null || item.data == null) return;

        if (item.data.baseStats.attack != 0)
            baseStats[StatType.Attack]
                .TryAddModifier(new StatModifier(item.data.baseStats.attack, StatModType.Flat, item));
        if (item.data.baseStats.defence != 0)
            baseStats[StatType.Defence]
                .TryAddModifier(new StatModifier(item.data.baseStats.defence, StatModType.Flat, item));
        if (item.data.baseStats.health != 0)
            baseStats[StatType.Health]
                .TryAddModifier(new StatModifier(item.data.baseStats.health, StatModType.Flat, item));
        if (item.data.baseStats.energy != 0)
            baseStats[StatType.Energy]
                .TryAddModifier(new StatModifier(item.data.baseStats.energy, StatModType.Flat, item));
        if (item.data.baseStats.speed != 0)
            baseStats[StatType.Speed].TryAddModifier(new StatModifier(item.data.baseStats.speed, StatModType.Flat, item));
    }
}