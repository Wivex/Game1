﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// NOTE: struct for easier saving?
// TODO: implement usage in mayor panel
public struct GameStatistics
{
    public float timePlayed;
    public int totalExpeditions;
    public int successfulExpeditions;
    public int failedExpeditions;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<LocationData> startingLocations = new List<LocationData>();
    //[Header("Expedition Settings")]
    [Tooltip("Minimal time in seconds between situations")]
    public int minGracePeriod = 4;

    internal List<Hero> heroes = new List<Hero>();
    internal Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();
    // TODO: load previous values from save
    internal GameStatistics gameStats = new GameStatistics();
    // global initiative accumulation speed
    internal float combatSpeed = 0.075f;
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

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewExpedition()
    {
        StartNewExpedition(new Hero(), startingLocations.FirstOrDefault());
    }

    public void StartNewExpedition(Hero hero, LocationData location)
    {
        var exp = new Expedition(hero, location);
    }

    void Update()
    {   
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituation();
    }
}