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

// TODO: move to corresponding drawer
internal struct HeroRedrawFlags
{
    internal bool stats, equipment, inventory, description, gold;
}

public class Hero : Unit
{
    internal string name;
    internal SexType sexType;
    internal ClassType classType;
    internal HeroState state;
    internal ClassData classData;
    internal int level, gold, experience;
    internal Sprite portrait;
    internal EquipmentSheet equipment = new EquipmentSheet();
    internal List<ItemData> backpack = new List<ItemData>();
    internal List<Consumable> consumables = new List<Consumable>();
    internal HeroRedrawFlags redrawFlags = new HeroRedrawFlags();

    // USE: TownManager.i.CreateNewHero()
    internal Hero(string name = default,
                  SexType sexType = default,
                  ClassType classType = default,
                  Sprite portrait = default)
    {
        this.sexType = sexType;
        this.portrait = portrait ?? RandomPortrait();
        this.classType = classType;
        this.name = name ?? NamingManager.statics.GetRandomHeroName(this);
        classData = Resources.Load<ClassData>(
            $"Units/Heroes/Classes/{classType.ToString()}/{classType.ToString()}Class");
        //HACK: temp solution
        state = HeroState.Recruitable;
        InitData(classData.classLevels[level]);
    }

    Sprite RandomPortrait()
    {
        var portraits =
            Resources.LoadAll<Sprite>($"Units/Heroes/Classes/{classType.ToString()}/Portraits/{sexType.ToString()}");
        return portraits[Random.Range(0, portraits.Length - 1)];
    }
}