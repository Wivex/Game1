using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPanelManager : UnitPanelManager
{
    [HideInInspector]
    public Hero hero;

    [Header("Hero")]
    public Image goldImage;
    public TextMeshProUGUI className,gold;
    public List<Sprite> goldSprites;

    public Transform backpackPanel;

    [HideInInspector]
    public Image[] inventorySlots,
        backpackSlots,
        effects,
        abilities;

    protected override void OnValidate()
    {
        backpackSlots = backpackPanel.gameObject.GetComponentsInChildren<Image>();
    }

    // NOTE: performance?
    void UpdateInventoryPanel()
    {
        for (var i = 0; i < inventorySlots.Length; i++)
            inventorySlots[i].sprite = hero.inventory[i].icon;
    }

    // NOTE: performance?
    void UpdateBackpackPanel()
    {
        for (var i = 0; i < backpackSlots.Length; i++)
            backpackSlots[i].sprite = hero.backpack[i].icon;
    }

    void Start()
    {
        // NOTE: check if safe
        hero = GameManager.expeditions.Last().Key;
        hero.SetStats();
        unit = hero;
        unitImage.sprite = hero.classData.classLevels[hero.level].icon;
        unitName.text = hero.name;
        className.text = hero.classData.classLevels[hero.level].name;
    }

    protected override void Update()
    {
        base.Update();
        
        gold.text = hero.gold.ToString();
        //PERF: check performance?
        var a = 0;
        var i = 0;
        while (hero.gold > a)
        {
            a = (int)Mathf.Pow(2, i++);
            if (i >= goldSprites.Count-1) break;
        }
        goldImage.sprite = goldSprites[i];

        UpdateInventoryPanel();
        UpdateBackpackPanel();
    }
}