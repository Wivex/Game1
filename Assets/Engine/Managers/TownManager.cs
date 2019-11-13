using System.Collections.Generic;
using Lexic;
using UnityEngine;

public static class TownManager
{
    internal static List<Hero> heroes = new List<Hero>();

    internal static List<Hero> IdleHeroes => heroes.FindAll(hero => hero.state == HeroState.Idle);
    internal static List<Hero> RecruitableHeroes => heroes.FindAll(hero => hero.state == HeroState.Recruitable);

    public static Hero NewHero(string name = default,
                              SexType sexType = default,
                              HeroClassType heroClassType = default,
                              Sprite portrait = default)
    {
        var hero = new Hero(name, sexType, heroClassType, portrait);
        heroes.Add(hero);
        return hero;
    }

    public static Hero NewHeroDebug()
    {
        return NewHero(null, SexType.Male, HeroClassType.Warrior);
    }
}