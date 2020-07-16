using System;
using System.Collections.Generic;
using Lexic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    internal static List<Hero> RecruitableHeroes => heroes.FindAll(hero => hero.state == HeroState.Recruit);

    public static Hero GenerateRandomHero()
    {
        var sex = (SexType)Random.Range(0, Enum.GetValues(typeof(SexType)).Length);
        var classType = (ClassType)Random.Range(0, Enum.GetValues(typeof(ClassType)).Length);
        var name = NamingManager.i.GetRandomHeroName(sex);
        var portraits =
            Resources.LoadAll<Sprite>($"Units/Heroes/Classes/{heroClassType.ToString()}/Portraits/{sex.ToString()}");
        return portraits[Random.Range(0, portraits.Length - 1)];
        return NewHero(name, sex, classType);
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

    public static Hero NewHero(string name = default,
        SexType sexType = default,
        ClassType classType = default,
        Sprite portrait = default)
    {
        var hero = new Hero(name, sexType, classType, portrait);
        heroes.Add(hero);
        return hero;
    }
}