using System.Collections.Generic;
using Lexic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    #region STATIC REFERENCE INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static TownManager i;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (i == null)
            //if not, set instance to this
            i = this;
        //If instance already exists and it's not this:
        else if (i != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region SET IN INSPECTOR

    //[Tooltip("Minimum time in seconds between events")]
    //public int minGracePeriod;

    #endregion

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

    public static Hero GenerateRandomHero()
    {
        return NewHero(null, SexType.Male, HeroClassType.Warrior);
    }

    // non-static for use in the Unity inspector
    public void NewIdleHeroesDebug(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var hero = GenerateRandomHero();
            hero.state = HeroState.Idle;
        }
    }
}