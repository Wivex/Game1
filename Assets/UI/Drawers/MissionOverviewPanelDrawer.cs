using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public class AnimationSequenceHandler
//{

//}


public class MissionOverviewPanelDrawer : Drawer
{
    #region SET IN INSPECTOR

    public Transform consumablesPanel;
    public Animator backAnim;
    public StatBar heroHpBar, heroEnergyBar, enemyHpBar, enemyEnergyBar;
    public Image heroImage, curGoldImage, heroIcon, encSubjectImage, encInteractionImage, locationImage, lootIcon, backOverlayImage;
    public Sprite townSprite;
    public CanvasGroup statBarsGroup;

    public TextMeshProUGUI heroName,
        level,
        gold;

    #endregion

    #region STATIC REFERENCES (CODE LOADED)

    // can't reference external scene objects in prefab inspector
    static List<Sprite> goldPileSprites;
    static CanvasManager missionsCMan;
    static Canvas overviewCanvas, detailsCanvas;

    #endregion

    internal Mission mission;

    Animator encAnim;
    AnimationMonitor animMonitor;

    internal static void CreateNew(Mission mis)
    {
        var newPanel =
            UIManager.i.prefabs.missionOverviewPanelPrefab.Instantiate<MissionOverviewPanelDrawer>(UIManager.i.panels.missionPreviewContentPanel);
        newPanel.Init(mis);
    }

    internal void Init(Mission mis)
    {
        this.mission = mis;

        // check once for all instances, cause these references are static for all panels
        if (missionsCMan == null)
        {
            goldPileSprites = Resources.LoadAll<Sprite>("Items/Gold").ToList();
            missionsCMan = UIManager.i.panels.missionPanel.GetComponent<CanvasManager>();
            detailsCanvas = missionsCMan.controlledCanvases.Find(canvas => canvas.name.Contains("Details"));
            overviewCanvas = UIManager.i.panels.missionPreviewContentPanel.GetComponent<Canvas>();
        }
        
        //auto-assign some references
        encAnim = GetComponent<Animator>();
        animMonitor = encAnim.GetBehaviour<AnimationMonitor>();

        // call OnPanelClicked() method when panel is clicked on
        GetComponent<Button>().onClick.AddListener(() => OnPanelClicked(mis));

        // event subscription
        animMonitor.AnimationsFinished += OnAnimationsFinished;
        mis.LocationChanged += OnLocationChanged;
        mis.EncounterStarted += OnEncounterStarted;
        mis.DamageTaken += OnDamageTaken;

        MissionIntroAnimSetUp();
    }

    void MissionIntroAnimSetUp()
    {
        locationImage.sprite = townSprite;
        encInteractionImage.enabled = false;
        encSubjectImage.enabled = false;
        encInteractionImage.enabled = false; 
        // set stat bars transparent
        statBarsGroup.enabled = true;
    }


    #region EVENT HANDLERS

    // used because can't pass class as parameter with button click from inspector
    void OnPanelClicked(Mission mis)
    {
        // expDetailsPanelDrawManager.InitHeroPanel(hero);
        missionsCMan.ChangeActiveCanvas(detailsCanvas);
    }

    /// <summary>
    /// New Mission begins with intro animation, then AnimationsFinished event control mission logic flow
    /// </summary>
    void OnAnimationsFinished()
    {
        mission.NextUpdate();
    }

    void OnLocationChanged()
    {
        locationImage.sprite = mission.route.NextLocationSprite();
    }

    void OnEncounterStarted(EncounterType type)
    {
        switch (type)
        {
            case EncounterType.None:
                encAnim.SetTrigger("No Encounter");
                break;
            case EncounterType.Enemy:
                // encInteractionImage = swords;
                encSubjectImage.enabled = true;
                encSubjectImage.sprite = (mission.curEncounter as EnemyEncounter).enemy.data.sprite;
                encInteractionImage.enabled = true;
                UIManager.TriggerAnimators($"{type} Encounter Start", encAnim, backAnim);
                break;

        }
    }


    void OnDamageTaken(Unit unit, Damage dam)
    {
        if (unit is Hero)
        {
            CreateFloatingText(heroIcon.transform, dam.amount);
        }
    }
    
    static void CreateFloatingText(Transform parentTransform, int value, Sprite background = null)
    {
        var floatingText = UIManager.i.prefabs.floatingTextPrefab.Instantiate<FloatingText>(parentTransform);
        if (value > 0)
        {
            floatingText.text = $"+{value}";
            floatingText.color = Color.green;
        }
        else if (value < 0)
        {
            floatingText.text = $"-{value}";
            floatingText.color = Color.red;
        }
        else
        {
            floatingText.text = "No effect";
            floatingText.color = Color.white;
        }
    }

    #endregion

    
    #region UI REDRAW METHODS

    /// <summary>
    /// Called after all animation is done. Can overwrite any "animator-locked" values (any parameters used in any state for any animation in the controller make them being overwritten with defaults on any other animator controller state).
    /// </summary>
    void LateUpdate()
    {
        // change hero sprite based on hero sex
        var curHeroSpriteIndex = mission.hero.data.spritesheet.IndexOf(heroIcon.sprite);
        if (mission.hero.sex == SexType.Female && curHeroSpriteIndex >= 50)
            heroIcon.sprite = mission.hero.data.spritesheet[curHeroSpriteIndex - 50];
    }
     
    void RedrawHeroDesc()
    {
        heroName.text = mission.hero.Name;
        level.text = $"Level {mission.hero.level} {mission.hero.data.name}";
    }

    void RedrawGold()
    {
        gold.text = mission.hero.gold.ToString();
        var a = 0;
        var index = 0;
        while (mission.hero.gold > a)
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
        if (mission.curEncounter is EnemyEncounter combat)
        {
            heroHpBar.SetTargetShiftingValue((float) combat.hero.HP / combat.hero.HPMax);
            heroEnergyBar.SetTargetShiftingValue((float) combat.hero.Energy / combat.hero.EnergyMax);
            enemyHpBar.SetTargetShiftingValue((float) combat.enemy.HP / combat.enemy.HPMax);
            enemyEnergyBar.SetTargetShiftingValue((float) combat.enemy.Energy / combat.enemy.EnergyMax);
        }
    }

    void RedrawSite()
    {
        // locationImage.sprite = mis.curSite.locationImage;
        // locationImage.rectTransform.sizeDelta = mis.curSite.areaImageSize;
        // locationImage.transform.localPosition = mis.curSite.zonesPositions[mis.curZoneIndex];
    }

    void RedrawEncounterSubject()
    {
        if (mission.curEncounter is EnemyEncounter combat)
            encSubjectImage.sprite = combat.enemy.data.sprite;
        if (mission.curEncounter is ContainerEncounter cont)
            encSubjectImage.sprite = cont.data.icon;
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