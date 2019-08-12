using System.Collections.Generic;
using System.Linq;

public static class ExpeditionManager
{
    internal static Dictionary<Hero, Expedition> expeditions = new Dictionary<Hero, Expedition>();

    public static void StartNewExpedition()
    {
        StartNewExpedition(new Hero(), GameManager.instance.startingLocations.First());
    }

    public static void StartNewExpedition(Hero hero, LocationData location)
    {
        var exp = new Expedition(hero, location);
        expeditions.Add(hero, exp);
    }

    internal static void Update()
    {
        foreach (var expedition in expeditions.Values)
            expedition.UpdateSituation();
    }
}