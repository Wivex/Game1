using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum HeroState
{
    Recruitable,
    Idle,
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
    //HACK: temp solution
    // TODO: use Lexic name generator
    readonly string[] heroNames = { "Peter", "Ron", "John", "Bob" };
    static int freeNameIndex;

    internal string name;
    internal TacticsPreset tactics;
    internal ClassData classData;
    internal int level, gold, experience;
    internal Sprite portrait;
    internal EquipmentSheet equipment = new EquipmentSheet();
    internal List<ItemData> backpack = new List<ItemData>();
    internal List<Consumable> consumables = new List<Consumable>();
    internal HeroState state = HeroState.Recruitable;

    public Hero(string name = default, Sprite portrait = default, HeroClass classType = default)
    {
        this.name = name ?? RandomName();
        this.portrait = portrait ?? RandomPortrait();
        classData = Resources.Load<ClassData>($"Units/Heroes/Classes/{classType.ToString()}/{classType.ToString()}Class");
        tactics = classData.classLevels[level].tacticsPreset;
        InitAbilities();
        this.InitStats();

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

    public override void InitAbilities()
    {
        foreach (var abilityData in classData.classLevels[level].abilities)
            abilities.Add(new Ability(abilityData));
    }
}