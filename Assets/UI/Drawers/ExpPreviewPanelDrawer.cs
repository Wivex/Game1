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

public class ExpPreviewPanelDrawer : MonoBehaviour, IPointerClickHandler
{
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

    Expedition exp;

    Image[] consumableSlots;
    TextMeshProUGUI[] consumablesCharges;

    public void Init(Expedition exp)
    {
        this.exp = exp;
        //auto assign all consumables slots to arrays
        var childImages = new List<Image>();
        consumablesPanel.gameObject.GetComponentsInChildren(true, childImages);
        consumableSlots = childImages.FindAll(image => image.gameObject.name.Contains("Image"))
            .ToArray();
        consumablesCharges = consumablesPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        
        // assign animation State Reference to all animatable objects, which should lock expedition logic, while animating
        heroAnim.GetComponent<AnimationManager>().animStateRef = exp.animStateRef;
        eventAnim.GetComponent<AnimationManager>().animStateRef = exp.animStateRef;
        interAnim.GetComponent<AnimationManager>().animStateRef = exp.animStateRef;
        lootAnim.GetComponent<AnimationManager>().animStateRef = exp.animStateRef;

        // TODO: should not run logic at initialization
        //UpdateHeroDesc();
        //UpdateGold();
        //UpdateStatBars();
        //UpdateConsumables();
    }

    //UNDONE
    //update UI panels
    void LateUpdate()
    {
        //if (redrawFlags.description)
        //    UpdateHeroDesc();
        //if (exp.hero.redrawFlags.gold)
        //    UpdateGold();
        //if (exp.hero.redrawFlags.consumes)
        //    UpdateConsumables();
        if (exp.redrawFlags.zone)
            UpdateZone();

        // NOTE: updated each frame anyway?
        UpdateStatBars();
    }

    // TODO: rename to Redraw
    #region UI UPDATE METHODS

    //void UpdateHeroDesc()
    //{
    //    heroName.text = hero.name;
    //    level.text = $"Level {hero.level} {hero.classData.classLevels[hero.level].name}";
    //    redrawFlags.description = false;
    //}

    //void UpdateGold()
    //{
    //    gold.text = hero.gold.ToString();
    //    var a = 0;
    //    var index = 0;
    //    while (hero.gold > a)
    //    {
    //        a = (int) Mathf.Pow(2, index++);
    //        if (index >= goldSprites.Count - 1) break;
    //    }

    //    curGoldImage.sprite = goldSprites[index];
    //    redrawFlags.gold = false;
    //}

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
        //redrawFlags.zone = false;
    }

    #endregion

    // using this to pass selected exp as parameter
    public void OnPointerClick(PointerEventData eventData)
    {
        // can't reference scene objects from prefab
        UIManager.i.expPanelDrawManager.ShowDetailsPanel(exp);
    }
}