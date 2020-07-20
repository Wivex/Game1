using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionOverviewPanelDrawer : Drawer
{
    #region SET IN INSPECTOR
    
    public Transform consumablesPanel, locationPanel;
    public FillingBar heroHpBar, heroEnergyBar, enemyHpBar, enemyEnergyBar;
    public Image heroImage, curGoldImage, heroIcon, encSubjectIcon, encInteractionIcon, locationImage, lootIcon;

    public TextMeshProUGUI heroName,
        level,
        gold;

    #endregion

    #region CODE LOADED
    
    // Image[] consumableIcons;
    // TextMeshProUGUI[] consumablesCharges;

    
    // can't reference external scene objects in prefab inspector
    static List<Sprite> goldPileSprites;
    static CanvasManager missionsCMan;
    static Canvas overviewCanvas, detailsCanvas;

    #endregion

    internal Mission mis;

    internal event Action AllAnimationsFinished;

    internal static List<MissionOverviewPanelDrawer> panelsList = new List<MissionOverviewPanelDrawer>();

    //auto-assign some references
    void Start()
    {
        // consumableIcons = consumablesPanel.GetComponentsInChildren<Image>().Where(comp => comp.name.Contains("Image")).ToArray();
        // consumablesCharges = consumablesPanel.GetComponentsInChildren<TextMeshProUGUI>();

        // call ShowDetailsPanel() method when panel is clicked on
        GetComponent<Button>().onClick.AddListener(() => ShowDetailsPanel(mis));

        // check once for all instances, cause references are static for all overview panels
        if (missionsCMan == null)
        {
            goldPileSprites = Resources.LoadAll<Sprite>("Items/Gold").ToList();
            missionsCMan = UIManager.i.panels.missionPanel.GetComponent<CanvasManager>();
            detailsCanvas = missionsCMan.controlledCanvases.Find(canvas => canvas.name.Contains("Details"));
            overviewCanvas = UIManager.i.panels.missionPreviewContentPanel.GetComponent<Canvas>();

            panelsList.Clear();
        }
    }

    internal void Init(Mission mis)
    {
        this.mis = mis;
        AllAnimationsFinished += mis.NextAction;

    }

    internal static void CreateNew(Mission mis)
    {
        var newPanel = UIManager.i.prefabs.missionOverviewPanelPrefab
                             .Instantiate<MissionOverviewPanelDrawer>(UIManager.i.panels.missionPreviewContentPanel);
        newPanel.Init(mis);
        panelsList.Add(newPanel);
    }

    // TODO: move draw to stat bars themselves
    public void SetStatBars()
    {
        var enemy = (mis.curEncounter as EnemyEncounter)?.enemy;
        heroHpBar.SetInitialValue((float)mis.hero.HP / mis.hero.HPMax);
        enemyHpBar.SetInitialValue((float)enemy.HP / enemy.HPMax);
        heroEnergyBar.SetInitialValue((float)mis.hero.Energy / mis.hero.EnergyMax);
        enemyEnergyBar.SetInitialValue((float)enemy.Energy / enemy.EnergyMax);
    }

    // can't pass class as parameter with button click from inspector
    internal void ShowDetailsPanel(Mission mis)
    {
        // expDetailsPanelDrawManager.InitHeroPanel(hero);
        missionsCMan.ChangeActiveCanvas(detailsCanvas);
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
            if (index >= goldPileSprites.Count - 1) break;
        }

        curGoldImage.sprite = goldPileSprites[index];
    }

    //void UpdateConsumables()
    //{
    //    for (var i = 0; i < consumableIcons.Length; i++)
    //    {
    //        if (i >= hero.consumables.Count)
    //        {
    //            consumableIcons[i].sprite = null;
    //            consumableIcons[i].color = Color.clear;
    //            consumablesCharges[i].text = string.Empty;
    //        }
    //        else
    //        {
    //            consumableIcons[i].sprite = hero.consumables[i].consumableData.icon;
    //            consumableIcons[i].color = Color.white;
    //            consumablesCharges[i].text = hero.consumables[i].curCharges.ToString();
    //        }
    //    }
    //}

    void RedrawStatBars()
    {
        if (mis.curEncounter is EnemyEncounter combat)
        {
            heroHpBar.TryUpdateValue((float)combat.hero.HP / combat.hero.HPMax);
            heroEnergyBar.TryUpdateValue((float)combat.hero.Energy / combat.hero.EnergyMax);
            enemyHpBar.TryUpdateValue((float)combat.enemy.HP / combat.enemy.HPMax);
            enemyEnergyBar.TryUpdateValue((float)combat.enemy.Energy / combat.enemy.EnergyMax);
        }
    }

    void RedrawSite()
    {
        // locationImage.sprite = mis.curSite.siteImage;
        // locationImage.rectTransform.sizeDelta = mis.curSite.areaImageSize;
        // locationImage.transform.localPosition = mis.curSite.zonesPositions[mis.curZoneIndex];
    }

    void RedrawEncounterSubject()
    {
        if (mis.curEncounter is EnemyEncounter combat)
            encSubjectIcon.sprite = combat.enemy.data.icon;
        if (mis.curEncounter is ContainerEncounter cont)
            encSubjectIcon.sprite = cont.data.icon;
    }

    void ChangeZoneImage()
    {
        // // check if last zone in the area
        // if (++curZoneIndex >= curSite.zonesPositions.Capacity)
        // {
        //     // UNDONE : temp unsafe solution (end overflow)
        //     curSite = curSite.interchangeable
        //         ? NewInterchangableSite
        //         : curZone.interchangebleSites[curZone.interchangebleSites.IndexOf(curSite) + 1];
        //     curZoneIndex = 0;
        // }
    }

    #endregion
}