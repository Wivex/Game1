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

public enum ClassType
{
    Warrior,
    Mage,
    Rogue
}

public enum SexType
{
    Male,
    Female
}

internal struct HeroRedrawFlags
{
    internal bool stats, equipment, inventory;
}

public class Hero : Unit
{
    internal string name;
    internal SexType sexType;
    internal ClassType classType;
    internal HeroState state;
    internal TacticsPreset tactics;
    internal ClassData classData;
    internal int level, gold, experience;
    internal Sprite portrait;
    internal EquipmentSheet equipment = new EquipmentSheet();
    internal List<ItemData> backpack = new List<ItemData>();
    internal List<Consumable> consumables = new List<Consumable>();
    internal HeroRedrawFlags redrawFlags = new HeroRedrawFlags();

    public Hero(string name = default, SexType sexType = default, ClassType classType = default, Sprite portrait = default)
    {
        this.name = name ?? RandomName();
        this.portrait = portrait ?? RandomPortrait();
        this.sexType = sexType;
        this.classType = classType;
        classData = Resources.Load<ClassData>($"Units/Heroes/Classes/{classType.ToString()}/{classType.ToString()}Class");
        //HACK: temp solution
        tactics = classData.classLevels[level].tacticsPreset;
        state = HeroState.Recruitable;

        UnitManager.statics.InitStats(this, classData.classLevels[level]);
        UnitManager.statics.InitAbilities(this, classData.classLevels[level]);

        // add hero to tavern roster
        TownManager.statics.heroes.Add(this);
    }

    string RandomName()
    {
        switch (sexType)
        {
            case SexType.Male:
                return ExpeditionManager.statics.maleNameGenerator.GetNextRandomName();
            case SexType.Female:
                return ExpeditionManager.statics.femaleNameGenerator.GetNextRandomName();
            default:
                return string.Empty;
        }
    }

    public Sprite RandomPortrait()
    {
        var portraits =
            Resources.LoadAll<Sprite>($"Units/Heroes/Classes/{classType.ToString()}/Portraits/{sexType.ToString()}");
        return portraits[Random.Range(0, portraits.Length - 1)];
    }
}