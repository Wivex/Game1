using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ExpeditionPanelDrawer expPanel;

    public static List<Unit> heroes = new List<Unit>();
    public static Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();

    public static float combatSpeed = 0.05f;
    public static float oldCombatSpeed;

    public void StartNewExpedition()
    {
        var hero = new Hero("Oswald");
        heroes.Add(hero);
        StartNewExpedition(hero);
    }

    // TODO: clean up assignment
    public void StartNewExpedition(Hero hero)
    {
        var expedition = new Expedition(expPanel, hero, LocationType.Dungeon);
        expeditions.Add(hero, expedition);
    }

    void FixedUpdate()
    {
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituations();
    }
}