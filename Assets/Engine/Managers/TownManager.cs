﻿using System.Collections.Generic;
using Lexic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static TownManager statics;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (statics == null)
            //if not, set instance to this
            statics = this;
        //If instance already exists and it's not this:
        else if (statics != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
    }

    #endregion

    internal List<Hero> heroes = new List<Hero>();

    internal List<Hero> IdleHeroes => heroes.FindAll(hero => hero.state == HeroState.Idle);
    internal List<Hero> RecruitableHeroes => heroes.FindAll(hero => hero.state == HeroState.Recruitable);

    public Hero CreateNewHero()
    {
        var hero = new Hero();
        heroes.Add(hero);
        return hero;
    }

    public void CreateNewHeroDebug()
    {
        var hero = new Hero();
        heroes.Add(hero);
    }
}