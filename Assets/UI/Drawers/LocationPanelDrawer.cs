﻿using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocationPanelDrawer : Drawer
{
    [HideInInspector]
    public ZoneData zone;

    Canvas canvas;
    Image locImage;
    TextMeshProUGUI locName;

    public Transform situationsPanel,
        enemiesPanel;

    List<Image> situationsIcons,
        enemiesIcons;

    List<TextMeshProUGUI> situationsName,
        situationsChance,
        enemiesName,
        enemiesChance;

    void OnValidate()
    {
        canvas = GetComponent<Canvas>();

        var childImages = new List<Image>();
        var childTexts = new List<TextMeshProUGUI>();

        situationsPanel.gameObject.GetComponentsInChildren(true, childImages);
        situationsIcons = childImages.FindAll(image => image.gameObject.name.Contains("Image"));
        situationsPanel.gameObject.GetComponentsInChildren(true, childTexts);
        situationsName = childTexts.FindAll(text => text.gameObject.name.Contains("Name"));
        situationsChance = childTexts.FindAll(text => text.gameObject.name.Contains("Chance"));

        enemiesPanel.gameObject.GetComponentsInChildren(true, childImages);
        enemiesIcons = childImages.FindAll(image => image.gameObject.name.Contains("Image"));
        enemiesPanel.gameObject.GetComponentsInChildren(true, childTexts);
        enemiesName = childTexts.FindAll(text => text.gameObject.name.Contains("Name"));
        enemiesChance = childTexts.FindAll(text => text.gameObject.name.Contains("Chance"));
    }

    void Update()
    {
        if (!canvas.enabled) return;

        // update events
        for (var i = 0; i < situationsIcons.Count; i++)
        {
            if (i >= zone.encounters.Count)
            {
                situationsIcons[i].sprite = null;
                situationsIcons[i].color = Color.clear;
                situationsName[i].text = string.Empty;
                situationsChance[i].text = string.Empty;
            }
            else
            {
                situationsIcons[i].color = Color.white;
                situationsName[i].text = zone.encounters[i].type.ToString();
                // situationsChance[i].text = zone.encounters[i].chanceWeight.ToString();
            }
        }

        // update enemies
        for (var i = 0; i < enemiesIcons.Count; i++)
        {
            if (i >= zone.enemies.Count)
            {
                enemiesIcons[i].sprite = null;
                enemiesIcons[i].color = Color.clear;
                enemiesName[i].text = string.Empty;
                enemiesChance[i].text = string.Empty;
            }
            else
            {
                enemiesIcons[i].sprite = zone.enemies[i].enemyData.sprite;
                enemiesIcons[i].color = Color.white;
                enemiesName[i].text = zone.enemies[i].enemyData.name;
                // enemiesChance[i].text = zone.enemies[i].chanceWeight.ToString();
            }
        }
    }
}