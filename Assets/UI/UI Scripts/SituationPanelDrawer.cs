using UnityEngine;
using TMPro;

public class SituationPanelDrawer : MonoBehaviour
{
    public Situation situation;

    public EnemyPanelDrawer enemyPanel;
    public HeroPanelDrawer heroPanel;
    public LocationPanelDrawer locationPanel;
    public CanvasManager canvasManager;

    public void InitEnemyPanel(Enemy enemy)
    {
        canvasManager.ChangeActiveCanvas(enemyPanel.gameObject.GetComponent<Canvas>());
        enemyPanel.Init(enemy);
    }

    public void InitLocationPanel(LocationData location)
    {
        canvasManager.ChangeActiveCanvas(locationPanel.gameObject.GetComponent<Canvas>());
        locationPanel.location = location;
    }
}