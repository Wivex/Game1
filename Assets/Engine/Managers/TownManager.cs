using System;
using System.Collections.Generic;
using System.Linq;
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

    public void GenerateNewIdleHeroesDebug(int count)
    {
        GenerateNewHeroes(HeroState.Idle, count);
    }

    internal static void GenerateNewHeroes(HeroState state, int count)
    {
        for (var j = 0; j < count; j++)
        {
            var hero = GenerateRandomHero();
            // can't be OnMission without mission set up
            hero.state = state == HeroState.OnMission ? HeroState.Idle : state;
            heroes.Add(hero);
        }
    }

    static Hero GenerateRandomHero()
    {
        var sex = (SexType)Random.Range(0, Enum.GetValues(typeof(SexType)).Length);
        var classType = (ClassType)Random.Range(0, Enum.GetValues(typeof(ClassType)).Length);
        var name = NamingManager.i.GetRandomHeroName(sex);
        var data = Resources.Load<HeroClassData>($"Units/Heroes/{classType}/{classType}");
        var portrait = data.portraits.Where(port => port.name.Contains($"{sex}")).PickOne();
        return new Hero(name, sex, portrait, data);
    }
}