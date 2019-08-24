using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct ExpeditionRedrawFlags
{
    public bool zone;
}

public class ExpPreviewPanelDrawer : MonoBehaviour, IPointerClickHandler, ICanvasVisibility
{
    #region SET IN INSPECTOR
    
    public List<Sprite> goldSprites;
    public Transform consumablesPanel;
    public Slider healthBar, manaBar, initBar, expBar;
    public Image heroImage, curGoldImage, heroIcon, objectIcon, interactionIcon, enemyStatusIcon, locationImage, lootIcon;

    public TextMeshProUGUI heroName,
        level,
        gold,
        experience,
        health,
        mana,
        initiative;

    #endregion

    Expedition exp;
    Image[] consumableSlots;
    TextMeshProUGUI[] consumablesCharges;

    public bool Visible { get; set; }

    //auto-assign some references
    void Awake()
    {
        consumableSlots = consumablesPanel.GetComponentsInChildren<Image>().Where(comp => comp.name.Contains("Image")).ToArray();
        consumablesCharges = consumablesPanel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Init(Expedition exp)
    {
        this.exp = exp;

        exp.heroAM = heroIcon.GetComponent<AnimationManager>();
        exp.objectAM = objectIcon.GetComponent<AnimationManager>();
        exp.interactionAM = interactionIcon.GetComponent<AnimationManager>();
        exp.lootAM = lootIcon.GetComponent<AnimationManager>();

        // let animators control animation state
        exp.anyAnimator.LinkToAnimationManagers(exp.heroAM, exp.interactionAM, exp.lootAM, exp.objectAM);
    }

    //update UI panels
    void LateUpdate()
    {
        if (!Visible) return;

        if (exp.hero.redrawFlags.description)
            RedrawHeroDesc();
        if (exp.hero.redrawFlags.gold)
            RedrawGold();
        //if (exp.hero.redrawFlags.consumes)
        //    UpdateConsumables();
        if (exp.redrawFlags.zone)
            UpdateZone();

        // NOTE: updated each frame anyway?
        UpdateStatBars();
    }

    // TODO: rename to Redraw
    #region UI REDRAW METHODS

    void RedrawHeroDesc()
    {
        heroName.text = exp.hero.name;
        level.text = $"Level {exp.hero.level} {exp.hero.classData.classLevels[exp.hero.level].name}";
        exp.hero.redrawFlags.description = false;
    }

    void RedrawGold()
    {
        gold.text = exp.hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (exp.hero.gold > a)
        {
            a = (int) Mathf.Pow(2, index++);
            if (index >= goldSprites.Count - 1) break;
        }

        curGoldImage.sprite = goldSprites[index];
        exp.hero.redrawFlags.gold = false;
    }

    //void UpdateConsumables()
    //{
    //    for (var i = 0; i < consumableSlots.Length; i++)
    //    {
    //        if (i >= hero.consumables.Count)
    //        {
    //            consumableSlots[i].sprite = null;
    //            consumableSlots[i].color = Color.clear;
    //            consumablesCharges[i].text = string.Empty;
    //        }
    //        else
    //        {
    //            consumableSlots[i].sprite = hero.consumables[i].consumableData.icon;
    //            consumableSlots[i].color = Color.white;
    //            consumablesCharges[i].text = hero.consumables[i].curCharges.ToString();
    //        }
    //    }
    //    redrawFlags.consumes = false;
    //}

    void UpdateStatBars()
    {
        //healthBar.value = (float) hero.baseStats[(int) StatType.Health].curValue /
        //                  (hero.baseStats[(int) StatType.Health] as StatChanging).maxValue;
        //health.text =
        //    $"{hero.baseStats[(int) StatType.Health].curValue} / {(hero.baseStats[(int) StatType.Health] as StatChanging).maxValue}";
        //manaBar.value = (float) hero.baseStats[(int) StatType.Energy].curValue /
        //                (hero.baseStats[(int) StatType.Energy] as StatChanging).maxValue;
        //mana.text =
        //    $"{hero.baseStats[(int) StatType.Energy].curValue} / {(hero.baseStats[(int) StatType.Energy] as StatChanging).maxValue}";
        //initBar.value = hero.curInitiative / ReqInitiative;
        //initiative.text = $"{(int) hero.curInitiative} / {ReqInitiative}";
        //expBar.value = (float) hero.experience / hero.classData.expPerLevel[hero.level];
        //experience.text = $"{hero.experience} / {hero.classData.expPerLevel[hero.level]}";
    }

    void UpdateZone()
    {
        locationImage.sprite = exp.curArea.areaImage;
        locationImage.rectTransform.sizeDelta = exp.curArea.areaImageSize;
        locationImage.transform.localPosition = exp.curArea.zonesPositions[exp.curZoneIndex];
        exp.redrawFlags.zone = false;
    }

    #endregion

    // using this to pass selected exp as parameter
    public void OnPointerClick(PointerEventData eventData)
    {
        // can't reference scene objects from prefab
        UIManager.i.expPanelDrawManager.ShowDetailsPanel(exp);
    }
}