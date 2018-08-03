using UnityEngine;
using TMPro;

public class SituationPanelDrawer : MonoBehaviour
{
    public Situation situation;

    public HeroPanelDrawer heroPanel;
    public EnemyPanelDrawer enemyPanel;
    public LocationPanelDrawer locationPanel;
    public CanvasManager canvasManager;

    public void InitHeroPanel(Hero hero)
    {
        canvasManager.AddCanvasesToDefaultActive(heroPanel.GetComponent<Canvas>());
        heroPanel.Init(hero);
    }

    public void InitEnemyPanel(Enemy enemy)
    {
        canvasManager.ChangeActiveCanvases(enemyPanel.GetComponent<Canvas>());
        enemyPanel.Init(enemy);
    }

    public void InitLocationPanel(LocationData location)
    {
        canvasManager.ChangeActiveCanvases(locationPanel.GetComponent<Canvas>());
        locationPanel.location = location;
    }
}