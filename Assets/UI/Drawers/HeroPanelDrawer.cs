﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Profiling;

public class HeroPanelDrawer : UnitPanelDrawer
{
    [Header("Hero")]
    public Hero hero;
    public Image curGoldImage;
    public TextMeshProUGUI classLevel, goldAmount, experience;
    public List<Sprite> goldSprites;

    public Transform backpackPanel,
        consumablesPanel,
        inventoryPanel;

    Image[] inventorySlots,
        backpackSlots,
        consumableSlots;

    [Disabled]
    TextMeshProUGUI[] consumablesCharges;

    public Slider expBar;

    // used to auto fill-in data in editor
    protected override void OnValidate()
    {
        base.OnValidate();

        var childImages = new List<Image>();
        backpackPanel.gameObject.GetComponentsInChildren(true, childImages);
        backpackSlots = childImages.FindAll(image => image.gameObject.name.Contains("Image"))
            .ToArray();
        consumablesPanel.gameObject.GetComponentsInChildren(true, childImages);
        consumableSlots = childImages.FindAll(image => image.gameObject.name.Contains("Image"))
            .ToArray();
        inventoryPanel.gameObject.GetComponentsInChildren(true, childImages);
        inventorySlots = childImages.FindAll(image => image.gameObject.name.Contains("Image"))
            .ToArray();
        consumablesCharges = consumablesPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Init(Hero hero)
    {
        this.hero = hero;
        unit = hero;
        // unitImage.sprite = hero.data.icon;
        unitName.text = hero.Name;
        classLevel.text = $"Level {hero.level} {hero.data.name}";
    }

    protected override void Update()
    {
        if (!canvas.enabled) return;

        Profiler.BeginSample("My Sample");

        base.Update();

        // update mis bar
        expBar.value = (float) hero.experience / 100;
        experience.text = $"{hero.experience} / 100";

        // update gold
        //NOTE: check performance?
        goldAmount.text = hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (hero.gold > a)
        {
            a = (int) Mathf.Pow(2, index++);
            if (index >= goldSprites.Count - 1) break;
        }

        curGoldImage.sprite = goldSprites[index];

        // update backpack
        for (var i = 0; i < backpackSlots.Length; i++)
        {
            if (i >= hero.backpack.Count)
            {
                backpackSlots[i].sprite = null;
                backpackSlots[i].color = Color.clear;
            }
            else
            {
                backpackSlots[i].sprite = hero.backpack[i].data.icon;
                backpackSlots[i].color = Color.white;
            }
        }

        // update equipment
        for (var i = 0; i < inventorySlots.Length; i++)
        {
            //if (hero.equipment[i] == null)
            //{
            //    inventorySlots[i].sprite = null;
            //    inventorySlots[i].color = Color.clear;
            //}
            //else
            //{
            //    inventorySlots[i].sprite = hero.equipment[i].icon;
            //    backpackSlots[i].color = Color.white;
            //}
        }

        // update consumables
        for (var i = 0; i < consumableSlots.Length; i++)
        {
            if (i >= hero.consumables.Count)
            {
                consumableSlots[i].sprite = null;
                consumableSlots[i].color = Color.clear;
                consumablesCharges[i].text = string.Empty;
            }
            else
            {
                consumableSlots[i].sprite = hero.consumables[i].data.icon;
                consumableSlots[i].color = Color.white;
                consumablesCharges[i].text = hero.consumables[i].charges.ToString();
            }
        }

        Profiler.EndSample();
    }
}