using System.Collections.Generic;
using System.Linq;
using Lexic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static ExpeditionManager statics;

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

    public NameGenerator maleNameGenerator, femaleNameGenerator;

    //[Header("Expedition Settings")]
    [Tooltip("Minimal time in seconds between situations")]
    public int minGracePeriod = 4;
    // global initiative accumulation speed
    public float combatSpeed = 0.075f;

    internal List<Expedition> expeditions = new List<Expedition>();

    float oldCombatSpeed;

    public void StartNewExpedition()
    {
        var loc = Resources.Load<LocationData>($"Locations/Outskirts/Outskirts");
        StartNewExpedition(new Hero(), loc);
    }

    public void StartNewExpedition(Hero hero, LocationData location)
    {
        expeditions.Add(new Expedition(hero, location));
    }

    void Update()
    {
        foreach (var expedition in expeditions)
            expedition.Update();
    }
}