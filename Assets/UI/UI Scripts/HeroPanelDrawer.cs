using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPanelDrawer : UnitPanelDrawer
{
    [Header("Hero")]
    public Hero hero;
    public Image curGoldImage;
    public TextMeshProUGUI className,goldAmount;
    public List<Sprite> goldSprites;

    public Transform backpackPanel,
        consumablesPanel;

    Image[] inventorySlots,
        backpackSlots,
        consumables;

    [Disabled]
    TextMeshProUGUI[] consumablesCharges;

    protected override void OnValidate()
    {
        base.OnValidate();

        var childImages = new List<Image>();
        backpackPanel.gameObject.GetComponentsInChildren(true, childImages);
        backpackSlots = childImages.FindAll(image => image.gameObject.name.Contains("Image")).ToArray();
        consumablesPanel.gameObject.GetComponentsInChildren(true, childImages);
        consumables = childImages.FindAll(image => image.gameObject.name.Contains("Image")).ToArray();
        consumablesCharges = consumablesPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Init(Hero hero)
    {
        this.hero = hero;
        unit = hero;
        unitImage.sprite = hero.classData.classLevels[hero.level].icon;
        unitName.text = hero.name;
        className.text = hero.classData.classLevels[hero.level].name;
    }

    protected override void Update()
    {
        if (!hero.spawned) return;

        base.Update();

        //NOTE: check performance?
        goldAmount.text = hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (hero.gold > a)
        {
            a = (int)Mathf.Pow(2, index++);
            if (index >= goldSprites.Count-1) break;
        }
        curGoldImage.sprite = goldSprites[index];
        
        // update backpack
        for (var i = 0; i < backpackSlots.Length; i++)
            backpackSlots[i].sprite = hero.backpack[i].icon;

        // update inventory
        for (var i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].sprite = hero.inventory[i].icon;
        }
    }
}