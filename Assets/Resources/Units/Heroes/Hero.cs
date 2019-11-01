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

        data = Resources.Load<HeroData>(
            $"Units/Heroes/Classes/{heroClassType.ToString()}/{heroClassType.ToString()}Class");
        //HACK: temp solution
        state = HeroState.Recruitable;
        InitData(data);
    }

    Sprite RandomPortrait()
    {
        var portraits =
            Resources.LoadAll<Sprite>($"Units/Heroes/Classes/{heroClassType.ToString()}/Portraits/{sexType.ToString()}");
        return portraits[Random.Range(0, portraits.Length - 1)];
    }
}