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

    public Transform consumablesPanel;
    public Animator animatorBackground;
    public StatBar heroHpBar, heroEnergyBar, enemyHpBar, enemyEnergyBar;
    public Image heroPortrait,
        curGoldImage,
        heroImage,
        encSubjectImage,
        encInteractionImage,
        locationImage,
        backOverlayImage;
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

    internal Mission mis;

    AnimatorHandler unitsAnim;
    /// <summary>
    /// These animations have to be finished (removed from collection) before running next logic iteration.
    /// </summary>
    HashSet<string> keyAnimations = new HashSet<string>();

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
        unitsAnim = GetComponent<AnimatorHandler>();

        // call OnPanelClicked() method when panel is clicked on
        GetComponent<Button>().onClick.AddListener(() => OnPanelClicked(mis));

        MissionEventsSubscription();
        MissionIntroAnimSetUp();
    }

    void KeyAnimationStart(Animator animator, string triggerMessage)
    {
        keyAnimations.Add(animator.ToString());
        animator.SetTrigger(triggerMessage);
    }

    void KeyAnimationEnd(Animator animator)
    {
        keyAnimations.Remove(animator.ToString());
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


    #region EVENT SUBSCRIPTIONS

    void MissionEventsSubscription()
    {
        unitsAnim.animMonitor.AnimationFinished += OnAnimationFinished;
        mis.LocationChanged += OnLocationChanged;
        mis.EncounterStarted += OnEncounterStarted;
        mis.UnitTookDamage += OnUnitTookDamage;
    }

    void CombatEventsSubscription()
    {
        var combat = mis.curEncounter as Combat;
        combat.DamageTaken += OnUnitTookDamage;
        combat.ActorActionPicked += OnActorActionPicked;
    }

    void AbilityAnimationEventSubscription(AnimatorHandler handler)
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
        var animName = animator.ToString();
        if (keyAnimations.Contains(animName)) keyAnimations.Remove(animName);
        if (keyAnimations.Count == 0) mis.NextUpdate();
    }

    /// <summary>
    /// Allows to run next mission logic update during key animation (apply ability effects, etc.). Appropriate logic action has to be prepared before that, to happen during this update.
    /// </summary>
    void OnAnimationEvent()
    {
        mis.NextUpdate();
    }

    void OnLocationChanged()
    {
        locationImage.sprite = mis.route.curLocSprite;
    }

    void OnActorActionPicked(TacticAction action)
    {
        var actorType = mis.Combat.actor is Hero ? "Hero" : "Enemy";
        var actionName = mis.Combat.curAction.actionType;
        // start units animations
        KeyAnimationStart(unitsAnim.animator, $"{actorType} {actionName}");
        // start picked action ability animation
        switch (action.actionType)
        {
            case ActionType.UseAbility:
                var ability = mis.Combat.actor.abilities.Find(abil => abil.data.name == action.ability);
                var abilityAnimHandler =
                    ability.data.animationPrefab.InstantiateAndGetComp<AnimatorHandler>(locationImage.transform.parent);
                keyAnimations.Add(abilityAnimHandler.animator.ToString());
                AbilityAnimationEventSubscription(abilityAnimHandler);
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
                KeyAnimationStart(unitsAnim.animator, "No Encounter");
                break;
            case EncounterType.Combat:
                encSubjectImage.enabled = true;
                encSubjectImage.sprite = (mis.curEncounter as Combat).enemy.data.sprite;
                encInteractionImage.enabled = true;
                CombatEventsSubscription();
                animatorBackground.SetTrigger("Combat Start");
                KeyAnimationStart(unitsAnim.animator, "Combat Start");
                break;
        }
    }

    void OnUnitTookDamage(Unit unit, Damage dam)
    {
        CreateFloatingText(unit is Hero ? heroImage.transform : encSubjectImage.transform, dam.amount);
    }

    void CreateFloatingText(Transform parentTransform, int value, Sprite background = null)
    {
        var floatingText = UIManager.i.prefabs.floatingTextPrefab.InstantiateAndGetComp<FloatingText>(parentTransform);
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

    /// <summary>
    /// Called after all animation is done. Can overwrite any "animator-locked" values (any parameters used in any state for any animation in the controller make them being overwritten with defaults on any other animator controller state).
    /// </summary>
    void LateUpdate()
    {
        // change hero sprite based on hero sex
        var curHeroSpriteIndex = mis.hero.data.spritesheet.IndexOf(heroImage.sprite);
        if (mis.hero.sex == SexType.Female && curHeroSpriteIndex >= 50)
            heroImage.sprite = mis.hero.data.spritesheet[curHeroSpriteIndex - 50];
    }
}