using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static List<Unit> heroes = new List<Unit>();
    public static Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();

    [Header("Expedition Settings")]
    [Tooltip("Minimal time in seconds between situations")]
    public int minGracePeriod = 5;

    // global initiative accumulation speed
    internal float combatSpeed = 0.05f;
    internal float oldCombatSpeed;

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

    public void StartNewExpedition(Hero hero)
    {
        var exp = new Expedition(hero, LocationType.Forest);
        expeditions.Add(hero, exp);
        UIManager.instance.expPanelDrawer.NewPreviewPanel(exp);
    }

    void Update()
    {   
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituations();
    }
}