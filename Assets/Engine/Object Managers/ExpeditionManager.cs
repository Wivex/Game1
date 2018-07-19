using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public RectTransform expeditionsListPanel;
    public ExpeditionPanelManager expeditionPanelPrefab;

    public static List<Unit> heroes = new List<Unit>();
    public static Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();

    public static float combatSpeed = 0.1f;
    public static float gameSpeed = 1;

    public void StartNewExpedition()
    {
        var hero = new Hero("Oswald");
        heroes.Add(hero);
        StartNewExpedition(hero);
    }

    // TODO: clean up assignment
    public void StartNewExpedition(Hero hero)
    {
        var expManager = Instantiate(expeditionPanelPrefab);
        expManager.transform.SetParent(expeditionsListPanel, false);

        var expedition = new Expedition(expManager, hero, LocationType.Dungeon);
        expeditions.Add(hero, expedition);

        expManager.expedition = expedition;
        expManager.expedition.situation = new SituationTravelling(expedition);
    }

    void FixedUpdate()
    {
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituations();
    }
}