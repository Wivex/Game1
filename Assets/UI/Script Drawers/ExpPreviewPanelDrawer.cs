using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct ExpPreviewPanelRedrawFlags
{
    public bool description,
        gold,
        consumes,
        zone;

    // eventPanel is redrawn every frame anyway
}

public class ExpPreviewPanelDrawer : MonoBehaviour, IPointerClickHandler
{
    //HACK: temp value
    public const int ReqInitiative = 100;

    #region SET IN INSPECTOR

    public List<Sprite> goldSprites;
    public Transform consumablesPanel;
    public Slider healthBar, manaBar, initBar, expBar;
    public Image heroImage, curGoldImage, heroIcon, eventIcon, enemyStatusIcon, locationImage, lootIcon;
    public Animator heroAnim, eventAnim, interAnim, lootAnim;

    public TextMeshProUGUI heroName,
        level,
        gold,
        experience,
        health,
        mana,
        initiative;

    #endregion

    //hide from inspector
    internal ExpPreviewPanelRedrawFlags redrawFlags;

    Expedition exp;
    Hero hero;

    Image[] consumableSlots;
    TextMeshProUGUI[] consumablesCharges;

    public void Init(Expedition exp)
    {
        this.exp = exp;
        hero = exp.hero;

        //init consumables
        //auto assign all consumables slots to arrays
        var childImages = new List<Image>();
        consumablesPanel.gameObject.GetComponentsInChildren(true, childImages);
        consumableSlots = childImages.FindAll(image => image.gameObject.name.Contains("Image"))
            .ToArray();
        consumablesCharges = consumablesPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        heroImage.GetComponent<AnimationManager>().animStateRef = exp.situation.animStateRef;

        UpdateHeroDesc();
        UpdateGold();
        UpdateStatBars();
        UpdateConsumables();
    }

    public void NotifyAnimationEnded()
    {
        exp.AnimationEnded();
    }

    public void TryNewSituation()
    {
        exp.TryNewSituation();
    }

    //update UI panels
    void LateUpdate()
    {
        if (redrawFlags.description)
            UpdateHeroDesc();
        if (redrawFlags.gold)
            UpdateGold();
        if (redrawFlags.consumes)
            UpdateConsumables();
        if (redrawFlags.zone)
            UpdateZone();

        // NOTE: updated each frame anyway?
        UpdateStatBars();
    }

    #region UI UPDATE METHODS

    void UpdateHeroDesc()
    {
        heroName.text = hero.name;
        level.text = $"Level {hero.level} {hero.classData.classLevels[hero.level].name}";
        redrawFlags.description = false;
    }

    void UpdateGold()
    {
        gold.text = hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (hero.gold > a)
        {
            a = (int) Mathf.Pow(2, index++);
            if (index >= goldSprites.Count - 1) break;
        }

        curGoldImage.sprite = goldSprites[index];
        redrawFlags.gold = false;
    }

    void UpdateConsumables()
    {
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
                consumableSlots[i].sprite = hero.consumables[i].consumableData.icon;
                consumableSlots[i].color = Color.white;
                consumablesCharges[i].text = hero.consumables[i].curCharges.ToString();
            }
        }
        redrawFlags.consumes = false;
    }

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
        expBar.value = (float) hero.experience / hero.classData.expPerLevel[hero.level];
        experience.text = $"{hero.experience} / {hero.classData.expPerLevel[hero.level]}";
    }

    void UpdateZone()
    {
        locationImage.sprite = exp.curArea.areaImage;
        locationImage.rectTransform.sizeDelta = exp.curArea.areaImageSize;
        locationImage.transform.localPosition = exp.curArea.zonesPositions[exp.curZoneIndex];
        redrawFlags.zone = false;
    }

    #endregion

    // show details panel if preview is double clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        var expPanel = UIManager.statics.expPanelDrawer;

        expPanel.selectedExp = exp;
        expPanel.detailsPanelDrawer.InitHeroPanel(hero);
        expPanel.ShowSelectedExpDetailsPanel(exp);
    }
}