using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MissionOverviewPanelDrawer : Drawer
{
    #region SET IN INSPECTOR

    public Transform consumablesPanel;
    public StatBar heroHPBar, heroEnergyBar, heroAPBar, enemyHPBar, enemyEnergyBar, enemyAPBar;
    public Image heroPortrait,
        curGoldImage,
        heroImage,
        enemyImage,
        encInteractionImage,
        locationImage;
    public Sprite townSprite;
    public CanvasGroup statBarsGroup;
    public TextMeshProUGUI heroName, level, gold;
    public GridLayoutHandler
        heroNegativeEffects, heroPositiveEffects, enemyNegativeEffects, enemyPositiveEffects;

    #endregion

    #region STATIC REFERENCES (CODE LOADED)

    // can't reference external scene objects in prefab inspector
    static List<Sprite> goldPileSprites;
    static CanvasManager missionsCMan;
    static Canvas overviewCanvas, detailsCanvas;

    #endregion

    internal Mission mis;

    Animator animatorLocation;
    AnimatorHandler animHero, animEnemy;
    Transform heroTransform, enemyTransform;
    /// <summary>
    /// These animations have to be finished (removed from collection) before running next logic iteration.
    /// </summary>
    HashSet<Animator> busyAnimators = new HashSet<Animator>();

    List<GameObject> mirroredObjects  = new List<GameObject>();

    internal static void CreateNew(Mission mis)
    {
        var newPanel =
            UIManager.i.prefabs.missionOverviewPanelPrefab.InstantiateAndGetComp<MissionOverviewPanelDrawer>(
                UIManager.i.panels.missionPreviewContentPanel);
        newPanel.Init(mis);
    }

    internal void Init(Mission mis)
    {
        this.mis = mis;

        // check once for all instances, cause these references are static for all panels
        if (missionsCMan == null)
        {
            goldPileSprites = Resources.LoadAll<Sprite>("Items/Gold").ToList();
            missionsCMan = UIManager.i.panels.missionPanel.GetComponent<CanvasManager>();
            detailsCanvas = missionsCMan.controlledCanvases.Find(canvas => canvas.name.Contains("Details"));
            overviewCanvas = UIManager.i.panels.missionPreviewContentPanel.GetComponent<Canvas>();
        }

        //auto-assign some references
        animatorLocation = locationImage.transform.parent.GetComponent<Animator>();
        heroTransform = heroImage.transform.parent;
        animHero = heroTransform.GetComponent<AnimatorHandler>();
        animHero.animMonitor.AnimationFinished += OnAnimationFinished;
        enemyTransform = enemyImage.transform.parent;
        animEnemy = enemyTransform.GetComponent<AnimatorHandler>();
        animEnemy.animMonitor.AnimationFinished += OnAnimationFinished;

        // call OnPanelClicked() method when panel is clicked on
        GetComponent<Button>().onClick.AddListener(() => OnPanelClicked(mis));
        
        MissionEventsSubscription();
        UnitEventsSubscription(mis.hero);

        MissionIntroAnimSetUp();
    }

    void KeyAnimationsStart(string triggerMessage, params Animator[] animators)
    {
        foreach (var animator in animators)
        {
            busyAnimators.Add(animator);
            animator.SetTrigger(triggerMessage);
        }
    }

    void MissionIntroAnimSetUp()
    {
        locationImage.sprite = townSprite;
        encInteractionImage.enabled = false;
        enemyImage.enabled = false;
        encInteractionImage.enabled = false;
        // set stat bars transparent
        statBarsGroup.enabled = true;

        busyAnimators.Add(animHero.animator);
    }


    #region EVENT SUBSCRIPTIONS

    void UnitEventsSubscription(Unit unit)
    {
        unit.TookDamage += OnUnitTookDamage;
        unit.EffectAdded += OnEffectAdded;
        unit.EffectApplied += OnEffectApplied;
        unit.EffectRemoved += OnEffectRemoved;
        unit.HPChanged += OnUnitHPChanged;
        unit.EnergyChanged += OnUnitEnergyChanged;
        unit.APChanged += OnUnitAPChanged;
    }
    
    void MissionEventsSubscription()
    {
        mis.LocationChanged += OnLocationChanged;
        mis.EncounterStarted += OnEncounterStarted;
    }

    void CombatEventsSubscription()
    {
        var combat = mis.curEncounter as Combat;
        combat.ActorActionPicked += OnActorActionPicked;
        combat.NewCombatTurnStarted += OnNewCombatTurnStarted;
    }

    void AnimationHandlerEventSubscription(AnimatorHandler handler)
    {
        handler.animMonitor.AnimationFinished += OnAnimationFinished;
        handler.AnimationEvent += OnAnimationEvent;
    }

    #endregion


    #region EVENT HANDLERS

    // used because can't pass class as parameter with button click from inspector
    void OnPanelClicked(Mission mis)
    {
        // expDetailsPanelDrawManager.InitHeroPanel(hero);
        missionsCMan.ChangeActiveCanvas(detailsCanvas);
    }

    /// <summary>
    /// Check if finished animation is key one, then remove it from the set and run logic if no other key animations running
    /// </summary>
    void OnAnimationFinished(Animator animator)
    {
        if (busyAnimators.Contains(animator)) 
            busyAnimators.Remove(animator);
        if (busyAnimators.Count == 0) 
            mis.NextAction();
    }

    /// <summary>
    /// Allows to run next mission logic update during key animation (apply ability effects, etc.). Appropriate logic action has to be prepared before that, to happen during this update.
    /// </summary>
    void OnAnimationEvent()
    {
        mis.NextAction();
    }

    void OnLocationChanged()
    {
        locationImage.sprite = mis.route.curLocSprite;
    }

    void OnActorActionPicked(TacticAction action)
    {
        var actionName = mis.Combat.curAction.actionType;
        // start units animations
        var actorAnim = mis.Combat.actor is Hero ? animHero : animEnemy;
        KeyAnimationsStart($"{actionName}", actorAnim.animator);
        // start picked action ability animation
        switch (action.actionType)
        {
            case ActionType.UseAbility:
                // run ability animation, if it has one
                var ability = mis.Combat.actor.abilities.Find(abil => abil.data.name == action.ability);
                if (ability.data.animationPrefab != null)
                {
                    // run ability animation
                    var abilityAnimHandler =
                        ability.data.animationPrefab.InstantiateAndGetComp<AnimatorHandler>(locationImage.transform.parent);
                    // mark instantiated skill animation to be mirrored
                    if (mis.Combat.actor is Enemy)
                        mirroredObjects.Add(abilityAnimHandler.gameObject);
                    busyAnimators.Add(abilityAnimHandler.animator);
                    AnimationHandlerEventSubscription(abilityAnimHandler);
                }
                break;
            case ActionType.UseConsumable:
                break;
        }
    }

    void OnEncounterStarted(EncounterType type)
    {
        switch (type)
        {
            case EncounterType.None:
                KeyAnimationsStart("No Encounter", animHero.animator);
                break;
            case EncounterType.Combat:
                UnitEventsSubscription(mis.Combat.enemy);
                enemyImage.enabled = true;
                enemyImage.sprite = (mis.curEncounter as Combat).enemy.data.sprite;
                encInteractionImage.enabled = true;
                SetInitialStatBars();
                CombatEventsSubscription();
                animatorLocation.SetTrigger("Combat Start");
                KeyAnimationsStart("Combat Start", animHero.animator, animEnemy.animator);
                break;
        }
    }

    void OnUnitHPChanged(Unit unit)
    {
        var HPBar = unit is Hero ? heroHPBar : enemyHPBar;
        HPBar.SetTargetShiftingValue((float) unit.HP / unit.HPMax);
    }

    void OnUnitEnergyChanged(Unit unit)
    {
        var energyBar = unit is Hero ? heroEnergyBar : enemyEnergyBar;
        energyBar.SetTargetShiftingValue((float) unit.Energy / unit.EnergyMax);
    }

    void OnUnitAPChanged(Unit unit)
    {
        var APBar = unit is Hero ? heroAPBar : enemyAPBar;
        APBar.SetTargetShiftingValue((float) unit.AP / unit.APMax);
    }

    void OnNewCombatTurnStarted()
    {
        heroAPBar.SetInstantValue((float) mis.hero.AP / mis.hero.APMax);
        enemyAPBar.SetInstantValue((float) mis.Combat.enemy.AP / mis.Combat.enemy.APMax);
    }

    void SetInitialStatBars()
    {
        heroHPBar.SetInstantValue((float) mis.hero.HP / mis.hero.HPMax);
        enemyHPBar.SetInstantValue((float) mis.Combat.enemy.HP / mis.Combat.enemy.HPMax);
        heroEnergyBar.SetInstantValue((float) mis.hero.Energy / mis.hero.EnergyMax);
        enemyEnergyBar.SetInstantValue((float) mis.Combat.enemy.Energy / mis.Combat.enemy.EnergyMax);
        heroAPBar.SetInstantValue((float) mis.hero.AP / mis.hero.APMax);
        enemyAPBar.SetInstantValue((float) mis.Combat.enemy.AP / mis.Combat.enemy.APMax);
    }

    void OnEffectAdded(Unit unit, EffectOverTimeType effectType)
    {
        var targetPanel = unit is Hero ? 
            effectType.IsNegative ? heroNegativeEffects : heroPositiveEffects :
            effectType.IsNegative ? enemyNegativeEffects : enemyPositiveEffects;
        if (!targetPanel.HasObject(effectType))
        {
            var effectObj = new GameObject();
            var effectImage = effectObj.AddComponent<Image>();
            effectImage.sprite = effectType.icon;
            targetPanel.AddCell(effectType, effectObj);
        }
    }

    void OnEffectApplied(Unit unit, EffectOverTimeType effectType)
    {
        if (effectType.animationPrefab != null)
        {
            var parent = unit is Hero ? heroTransform : enemyTransform;
            var effectAnimHandler = effectType.animationPrefab.InstantiateAndGetComp<AnimatorHandler>(parent);
            busyAnimators.Add(effectAnimHandler.animator);
            AnimationHandlerEventSubscription(effectAnimHandler);
        }
        else
            mis.NextAction();
    }

    void OnEffectRemoved(Unit unit, EffectOverTimeType effectType)
    {
        var targetPanel = unit is Hero ? 
            effectType.IsNegative ? heroNegativeEffects : heroPositiveEffects :
            effectType.IsNegative ? enemyNegativeEffects : enemyPositiveEffects;
        targetPanel.RemoveCell(effectType);
    }

    // TODO: change to OnHPChanged
    void OnUnitTookDamage(Unit unit, Damage dam)
    {
        var parent = unit is Hero ? heroTransform : enemyTransform;
        if (dam.amount > 0)
        {
            FloatingText.Create(parent, $"-{dam.amount}", Color.red, dam.icon);
            var HPBar = unit is Hero ? heroHPBar : enemyHPBar;
            HPBar.SetTargetShiftingValue((float) unit.HP / unit.HPMax);
        }
        else
            FloatingText.Create(parent, "No effect", Color.white);
    }

    #endregion

    /// <summary>
    /// Called after all animation is done. Can overwrite any "animator-locked" values (any parameters used in any state for any animation in the controller make them being overwritten with defaults on any other animator controller state).
    /// </summary>
    void LateUpdate()
    {
        // change hero sprite based on hero sex
        var curHeroSpriteIndex = mis.hero.data.spritesheet.IndexOf(heroImage.sprite);
        if (mis.hero.sex == SexType.Female && curHeroSpriteIndex >= 50)
            heroImage.sprite = mis.hero.data.spritesheet[curHeroSpriteIndex - 50];

        // perform mirror transformation for mirrored objects after their animations
        for (var i = 0; i < mirroredObjects.Count; i++)
        {
            if (mirroredObjects[i] == null)
            {
                // remove from list if object destroyed
                mirroredObjects.Remove(mirroredObjects[i]);
                // index correction after removal
                i--;
            }
            else
                UIManager.MirrorByX(mirroredObjects[i].transform);
        }
    }
}