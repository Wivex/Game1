using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum HeroState
{
    Recruitable,
    Idle,
    OnExpedition
}

public enum HeroClassType
{
    Any,
    Warrior,
    Mage,
    Rogue
}

public enum SexType
{
    Male,
    Female
}

public class Hero : Unit
{
    internal string name;
    internal SexType sexType;
    internal HeroClassType heroClassType;
    internal HeroState state;
    internal HeroData data;
    internal int level, gold, experience;
    internal Sprite portrait;
    internal Equipment equipment = new Equipment();
    internal List<Item> backpack = new List<Item>();
    internal List<Item> consumables = new List<Item>();

    // USE: TownManager.i.NewHero()
    internal Hero(string name = default,
                  SexType sexType = default,
                  HeroClassType heroClassType = default,
                  Sprite portrait = default)
    {
        this.sexType = sexType;
        this.heroClassType = heroClassType;
        this.name = name ?? NamingManager.statics.GetRandomHeroName(this);
        this.portrait = portrait ?? RandomPortrait();

        //HACK: temp solution
        data = Resources.Load<HeroData>(
            $"Units/Heroes/Classes/{heroClassType.ToString()}/{heroClassType.ToString()}");
        //HACK: temp solution
        state = HeroState.Recruitable;
        InitData(data);
        InitEquipment();
    }

    // NOTE: non-reflection way to iterate equipment slots?
    void InitEquipment()
    {
        equipment = data.equipment;

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

        if (item.data.equipStats.attack != 0)
            stats[StatType.Attack]
                .TryAddModifier(new StatModifier(item.data.equipStats.attack, StatModType.Flat, item));
        if (item.data.equipStats.defence != 0)
            stats[StatType.Defence]
                .TryAddModifier(new StatModifier(item.data.equipStats.defence, StatModType.Flat, item));
        if (item.data.equipStats.health != 0)
            stats[StatType.Health]
                .TryAddModifier(new StatModifier(item.data.equipStats.health, StatModType.Flat, item));
        if (item.data.equipStats.energy != 0)
            stats[StatType.Energy]
                .TryAddModifier(new StatModifier(item.data.equipStats.energy, StatModType.Flat, item));
        if (item.data.equipStats.speed != 0)
            stats[StatType.Speed].TryAddModifier(new StatModifier(item.data.equipStats.speed, StatModType.Flat, item));
    }

    Sprite RandomPortrait()
    {
        var portraits =
            Resources.LoadAll<Sprite>($"Units/Heroes/Classes/{heroClassType.ToString()}/Portraits/{sexType.ToString()}");
        return portraits[Random.Range(0, portraits.Length - 1)];
    }
}