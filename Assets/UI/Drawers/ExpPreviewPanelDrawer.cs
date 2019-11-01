using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpPreviewPanelDrawer : MonoBehaviour, IPointerClickHandler, ICanvasVisibility
{
    #region SET IN INSPECTOR
    
    public List<Sprite> goldSprites;
    public Transform consumablesPanel, locationPanel;
    public FillingBar heroHpBar, heroEnergyBar, enemyHpBar, enemyEnergyBar;
    public Image heroImage, curGoldImage, heroIcon, objectIcon, interactionIcon, enemyStatusIcon, heroStatusIcon, locationImage, lootIcon;

    public TextMeshProUGUI heroName,
        level,
        gold;

    #endregion

    internal Expedition exp;

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

        exp.heroAM = heroIcon.GetComponent<AnimatorManager>();
        exp.enemyAM = objectIcon.GetComponent<AnimatorManager>();
        exp.interactionAM = interactionIcon.GetComponent<AnimatorManager>();
        exp.lootAM = lootIcon.GetComponent<AnimatorManager>();
        exp.locationAM = locationPanel.GetComponent<AnimatorManager>();
    }

    //update UI panels
    void LateUpdate()
    {
        if (!Visible) return;

        RedrawHeroDesc();
        RedrawGold();
        //    UpdateConsumables();
        UpdateZone();
        UpdateStatBars();
        UpdateUnitStatuses();
    }

    // TODO: rename to Redraw
    #region UI REDRAW METHODS

    void RedrawHeroDesc()
    {
        heroName.text = exp.hero.name;
        level.text = $"Level {exp.hero.level} {exp.hero.data.name}";
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
    //}

    void UpdateStatBars()
    {
        if (exp.curEncounter is Combat combat)
        {
            heroHpBar.TryUpdateValue((float)combat.hero.HP / combat.hero.HPMax);
            heroEnergyBar.TryUpdateValue((float)combat.hero.Energy / combat.hero.EnergyMax);
            enemyHpBar.TryUpdateValue((float)combat.enemy.HP / combat.enemy.HPMax);
            enemyEnergyBar.TryUpdateValue((float)combat.enemy.Energy / combat.enemy.EnergyMax);
        }
    }

    //UNDONE:
    void UpdateUnitStatuses()
    {
        if (exp.curEncounter is Combat combat)
        {
            heroStatusIcon.gameObject.SetActive(combat.hero.Dead);
            enemyStatusIcon.gameObject.SetActive(combat.enemy.Dead);
        }
    }

    void UpdateZone()
    {
        locationImage.sprite = exp.curArea.areaImage;
        locationImage.rectTransform.sizeDelta = exp.curArea.areaImageSize;
        locationImage.transform.localPosition = exp.curArea.zonesPositions[exp.curZoneIndex];
    }

    #endregion

    // using this to pass selected exp as parameter
    public void OnPointerClick(PointerEventData eventData)
    {
        // can't reference scene objects from prefab
        UIManager.i.expPanelDrawManager.ShowDetailsPanel(exp);
    }
}