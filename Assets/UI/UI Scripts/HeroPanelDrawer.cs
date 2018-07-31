using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    protected override void OnValidate()
    {
        base.OnValidate();

        var childImages = new List<Image>();
        backpackPanel.gameObject.GetComponentsInChildren(true, childImages);
        backpackSlots = childImages.FindAll(image => image.gameObject.name.Contains("Image"))
            .ToArray();
        consumablesPanel.gameObject.GetComponentsInChildren(true, childImages);
        consumableSlots = childImages.FindAll(image => image.gameObject.name.Contains("Consumable"))
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
        unitImage.sprite = hero.classData.classLevels[hero.level].icon;
        unitName.text = hero.name;
        classLevel.text = $"Level {hero.level} {hero.classData.classLevels[hero.level].name}";
    }

    protected override void Update()
    {
        if (!hero.spawned) return;

        base.Update();

        // update exp bar
        expBar.value = (float) hero.experience / hero.classData.expPerLevel[hero.level];
        experience.text = $"{hero.experience} / {hero.classData.expPerLevel[hero.level]}";

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
            if (i >= hero.backpack.Length || hero.backpack[i] == null)
            {
                backpackSlots[i].sprite = null;
                backpackSlots[i].color = Color.clear;
            }
            else
            {
                backpackSlots[i].sprite = hero.backpack[i].icon;
                backpackSlots[i].color = Color.white;
            }
        }

        // update inventory
        for (var i = 0; i < inventorySlots.Length; i++)
        {
            if (hero.inventory[i] == null)
            {
                inventorySlots[i].sprite = null;
                inventorySlots[i].color = Color.clear;
            }
            else
            {
                inventorySlots[i].sprite = hero.inventory[i].icon;
                backpackSlots[i].color = Color.white;
            }
        }

        // update consumables
        for (var i = 0; i < consumableSlots.Length; i++)
        {
            if (hero.consumables[i] == null)
            {
                consumableSlots[i].sprite = null;
                consumableSlots[i].color = Color.clear;
                consumablesCharges[i].text = string.Empty;
            }
            else
            {
                consumableSlots[i].sprite = hero.consumables[i].consumableData.icon;
                consumableSlots[i].color = Color.white;
                consumablesCharges[i].text = hero.consumables[i].curCharges.ToString();
            }
        }
    }
}