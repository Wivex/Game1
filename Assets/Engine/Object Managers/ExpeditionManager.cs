using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public RectTransform expeditionsListPanel, expeditionPanelPrefab;
    HeroPanelManager heroPanel;
    EnemyPanelManager enemyPanel;

    public static List<Unit> heroes = new List<Unit>();
    public static Dictionary<Hero,Expedition> expeditions = new Dictionary<Hero, Expedition>();

    public float combatSpeed = 0.1f;
    public float gameSpeed = 1;

    public void StartNewExpedition()
    {
        var hero = new Hero("Oswald");
        heroes.Add(hero);
        StartNewExpedition(hero);
    }

    public void StartNewExpedition(Hero hero)
    {
        var expeditionPanel = Instantiate(expeditionPanelPrefab);
        expeditionPanel.SetParent(expeditionsListPanel, false);
        expeditions.Add(hero, new Expedition(hero, Location.Dungeon));
    }

    void FixedUpdate()
    {

    }
}
