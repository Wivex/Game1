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
        consumes;

    // eventPanel is redrawn every frame anyway
}

public class ExpPreviewPanelDrawer : MonoBehaviour, IPointerClickHandler
{
    public const int reqInitiative = 100;

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
    internal ExpPreviewPanelRedrawFlags redrawFlags = new ExpPreviewPanelRedrawFlags();

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

        UpdateStatBars();
    }

    #region UI UPDATE METHODS

    void UpdateHeroDesc()
    {
        heroName.text = hero.name;
        level.text = $"Level {hero.level} {hero.classData.classLevels[hero.level].name}";
    }

    void UpdateGold()
    {
        gold.text = hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (hero.gold > a)
        {
            a = (int)Mathf.Pow(2, index++);
            if (index >= goldSprites.Count - 1) break;
        }

        curGoldImage.sprite = goldSprites[index];
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
    }

    void UpdateStatBars()
    {
        healthBar.value = (float)hero.stats[(int)StatType.Health].curValue /
                          hero.stats[(int)StatType.Health].curValue;
        health.text =
            $"{hero.stats[(int)StatType.Health].curValue} / {(hero.stats[(int)StatType.Health] as StatChanging).maxValue}";
        manaBar.value = (float)hero.stats[(int)StatType.Mana].curValue /
                        hero.stats[(int)StatType.Mana].curValue;
        mana.text =
            $"{hero.stats[(int)StatType.Mana].curValue} / {(hero.stats[(int)StatType.Mana] as StatChanging).maxValue}";
        initBar.value = hero.curInitiative / reqInitiative;
        initiative.text = $"{(int)hero.curInitiative} / {reqInitiative}";
        expBar.value = (float)hero.experience / hero.classData.expPerLevel[hero.level];
        experience.text = $"{hero.experience} / {hero.classData.expPerLevel[hero.level]}";
    }

    #endregion

    // show details panel if preview is double clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            var expPanel = UIManager.instance.expPanelDrawer;

            expPanel.selectedExp = exp;
            expPanel.detailsPanelDrawer.InitHeroPanel(hero);
            expPanel.ShowSelectedExpDetailsPanel(exp);
        }
    }
}