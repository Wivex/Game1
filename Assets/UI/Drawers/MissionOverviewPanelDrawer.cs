using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionOverviewPanelDrawer : Drawer, IPointerClickHandler
{
    #region SET IN INSPECTOR
    
    public Transform consumablesPanel, locationPanel;
    public FillingBar heroHpBar, heroEnergyBar, enemyHpBar, enemyEnergyBar;
    public Image heroImage, curGoldImage, heroIcon, objectIcon, interactionIcon, enemyStatusIcon, heroStatusIcon, locationImage, lootIcon;
    List<Sprite> goldSprites;

    public TextMeshProUGUI heroName,
        level,
        gold;

    #endregion

    internal Mission mis;

    Image[] consumableSlots;
    TextMeshProUGUI[] consumablesCharges;

    public bool Visible { get; set; }

    //auto-assign some references
    void Awake()
    {
        consumableSlots = consumablesPanel.GetComponentsInChildren<Image>().Where(comp => comp.name.Contains("Image")).ToArray();
        consumablesCharges = consumablesPanel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Init(Mission mis)
    {
        this.mis = mis;

        //mis.heroAM = heroIcon.GetComponent<AnimatorManager>();
        //mis.encounterAM = objectIcon.GetComponent<AnimatorManager>();
        //mis.interactionAM = interactionIcon.GetComponent<AnimatorManager>();
        //mis.lootAM = lootIcon.GetComponent<AnimatorManager>();
        //mis.locationAM = locationPanel.GetComponent<AnimatorManager>();

        //mis.CombatStartEvent += SetStatBars;
    }

    // required for proper "unlistening" of C# event
    protected void OnDestroy()
    {
        //if (mis != null)
        //    mis.CombatStartEvent -= SetStatBars;
    }

    //update UI panels
    void LateUpdate()
    {
        RedrawHeroDesc();
        RedrawGold();
        //    UpdateConsumables();
        UpdateZone();
        UpdateEncounterImage();
        UpdateStatBars();
        UpdateUnitStatuses();
    }

    public void SetStatBars()
    {
        var enemy = (mis.curEncounter as EnemyEncounter)?.enemy;
        heroHpBar.SetInitialValue((float)mis.hero.HP / mis.hero.HPMax);
        enemyHpBar.SetInitialValue((float)enemy.HP / enemy.HPMax);
        heroEnergyBar.SetInitialValue((float)mis.hero.Energy / mis.hero.EnergyMax);
        enemyEnergyBar.SetInitialValue((float)enemy.Energy / enemy.EnergyMax);
    }

    // TODO: rename to Redraw
    #region UI REDRAW METHODS

    void RedrawHeroDesc()
    {
        heroName.text = mis.hero.name;
        level.text = $"Level {mis.hero.level} {mis.hero.data.name}";
    }

    void RedrawGold()
    {
        gold.text = mis.hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (mis.hero.gold > a)
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
        if (mis.curEncounter is EnemyEncounter combat)
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
        if (mis.curEncounter is EnemyEncounter combat)
        {
            heroStatusIcon.gameObject.SetActive(combat.hero.Dead);
            enemyStatusIcon.gameObject.SetActive(combat.enemy.Dead);
        }
    }

    void UpdateZone()
    {
        // locationImage.sprite = mis.curSite.siteImage;
        // locationImage.rectTransform.sizeDelta = mis.curSite.areaImageSize;
        // locationImage.transform.localPosition = mis.curSite.zonesPositions[mis.curZoneIndex];
    }

    void UpdateEncounterImage()
    {
        if (mis.curEncounter is EnemyEncounter combat)
            objectIcon.sprite = combat.enemy.data.icon;
        if (mis.curEncounter is ContainerEncounter cont)
            objectIcon.sprite = cont.data.icon;
    }

    #endregion

    // using this to pass selected mis as parameter
    public void OnPointerClick(PointerEventData eventData)
    {
        // can't reference scene objects from prefab
        //UIManager.misPanelManager.ShowDetailsPanel(mis);
    }
}