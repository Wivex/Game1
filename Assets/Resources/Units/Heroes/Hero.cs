using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum HeroState
{
    Recruitable,
    InRoster,
    OnExpedition
}

public enum HeroClass
{
    Warrior,
    Mage,
    Rogue
}

[Serializable]
public class Hero : Unit
{
    readonly string[] heroNames = { "Peter", "Ron", "John", "Bob" };
    static int freeNameIndex;

    internal ClassData classData;
    internal int level, gold, experience;
    internal Sprite portrait;
    internal EquipmentData[] inventory = new EquipmentData[Enum.GetNames(typeof(InventorySlot)).Length];
    internal List<ItemData> backpack = new List<ItemData>();
    internal List<Consumable> consumables = new List<Consumable>();
    internal HeroState state = HeroState.Recruitable;

    public Hero(string name = default, Sprite portrait = default, HeroClass classType = default)
    {
        this.name = name ?? RandomName();
        this.portrait = portrait ?? RandomPortrait();
        classData = Resources.Load<ClassData>($"Units/Heroes/Classes/{classType.ToString()}/{classType.ToString()}Class");
        tacticsPreset = classData.classLevels[level].tacticsPreset;
        SetAbilities();
        SetStats();

        GameManager.instance.heroes.Add(this);
    }

    public string RandomName()
    {
        if (freeNameIndex == heroNames.Length)
            freeNameIndex = 0;
        return heroNames[freeNameIndex++];
    }

    public Sprite RandomPortrait()
    {
        var portraits = Resources.LoadAll<Sprite>($"Units/Heroes/Classes/Warrior/Portraits");
        var rnd = Random.Range(0, portraits.Length - 1);
        return portraits[rnd];
    }

    public override void SetStats()
    {
        stats[(int) StatType.Health].BaseValue = classData.classLevels[level].stats.health;
        stats[(int) StatType.Mana].BaseValue = classData.classLevels[level].stats.mana;
        stats[(int) StatType.Attack].BaseValue = classData.classLevels[level].stats.attack;
        stats[(int) StatType.Defence].BaseValue = classData.classLevels[level].stats.defence;
        stats[(int) StatType.Speed].BaseValue = classData.classLevels[level].stats.speed;
        stats[(int) StatType.HResist].BaseValue = classData.classLevels[level].stats.hazardResistance;
        stats[(int) StatType.BResist].BaseValue = classData.classLevels[level].stats.bleedResistance;

        stats[(int) StatType.Health].curValue = stats[(int) StatType.Health].BaseValue;
        stats[(int) StatType.Mana].curValue = stats[(int) StatType.Mana].BaseValue;
        stats[(int) StatType.Attack].curValue = stats[(int) StatType.Attack].BaseValue;
        stats[(int) StatType.Defence].curValue = stats[(int) StatType.Defence].BaseValue;
        stats[(int) StatType.Speed].curValue = stats[(int) StatType.Speed].BaseValue;
        stats[(int) StatType.HResist].curValue = stats[(int) StatType.HResist].BaseValue;
        stats[(int) StatType.BResist].curValue = stats[(int) StatType.BResist].BaseValue;
    }

    public override void SetAbilities()
    {
        foreach (var abilityData in classData.classLevels[level].abilities)
            abilities.Add(new Ability(abilityData));
    }
}