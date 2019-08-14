using System.Collections.Generic;
using Lexic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

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

    internal List<Hero> heroes = new List<Hero>();

    internal List<Hero> IdleHeroes => heroes.FindAll(hero => hero.state == HeroState.Idle);
    internal List<Hero> RecruitableHeroes => heroes.FindAll(hero => hero.state == HeroState.Recruitable);

    public Hero CreateNewHero(string name = default,
                              SexType sexType = default,
                              ClassType classType = default,
                              Sprite portrait = default)
    {
        var hero = new Hero(name, sexType, classType, portrait);
        heroes.Add(hero);
        return hero;
    }

    public void CreateNewHeroDebug()
    {
        CreateNewHero();
    }
}