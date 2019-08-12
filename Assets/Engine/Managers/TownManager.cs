using System.Collections.Generic;

public static class TownManager
{
    internal static List<Hero> heroes = new List<Hero>();

    internal static List<Hero> IdleHeroes => heroes.FindAll(hero => hero.state == HeroState.Idle);
    internal static List<Hero> RecruitableHeroes => heroes.FindAll(hero => hero.state == HeroState.Recruitable);
}