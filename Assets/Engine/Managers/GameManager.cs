using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ExpeditionPanelDrawer expPanel;

    public static List<Unit> heroes = new List<Unit>();
    public static Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();

    public static float combatSpeed = 0.05f;
    public static float oldCombatSpeed;

    public void Quit()
    {
        Application.Quit();
    }

    public void InitExpedition()
    {
        var hero = new Hero("Oswald");
        heroes.Add(hero);
        InitExpedition(hero);
    }

    // TODO: clean up assignment
    public void InitExpedition(Hero hero)
    {
        var expedition = new Expedition(expPanel, hero, LocationType.Dungeon);
        expeditions.Add(hero, expedition);
        expPanel.situationPanel.heroPanel.Init(hero);
    }

    void FixedUpdate()
    {
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituations();
    }
}