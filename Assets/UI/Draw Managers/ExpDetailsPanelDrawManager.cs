﻿using UnityEngine;
using TMPro;

//TODO: rework
public class ExpDetailsPanelDrawManager : MonoBehaviour
{
    public HeroPanelDrawer heroPanel;
    public EnemyPanelDrawer enemyPanel;
    public LocationPanelDrawer locationPanel;
    public CanvasManager canvasManager;

    public void InitHeroPanel(Hero hero)
    {
        heroPanel.Init(hero);
    }

    public void InitEnemyPanel(Enemy enemy)
    {
        canvasManager.ChangeActiveCanvas(enemyPanel.GetComponent<Canvas>());
        enemyPanel.Init(enemy);
    }

    public void InitLocationPanel(LocationData location)
    {
        canvasManager.ChangeActiveCanvas(locationPanel.GetComponent<Canvas>());
        locationPanel.location = location;
    }
}