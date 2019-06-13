using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static List<Unit> heroes = new List<Unit>();
    public static Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();

    public static float combatSpeed = 0.05f;
    public static float oldCombatSpeed;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewExpedition()
    {
        var hero = new Hero("Oswald");
        heroes.Add(hero);
        StartNewExpedition(hero);
    }

    // TODO: clean up assignment
    public void StartNewExpedition(Hero hero)
    {
        var expedition = new Expedition(hero, LocationType.Dungeon);
        expeditions.Add(hero, expedition);
        UIManager.instance.expPanelDrawer.AddExpedition(expedition);
    }

    private void FixedUpdate()
    {   
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituations();
    }
}