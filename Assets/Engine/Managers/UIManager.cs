using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class PrefabReferences
{
    public GameObject floatingTextPrefab, meleeHitEffectPrefab, missionOverviewPanelPrefab;
}

[Serializable]
public class UIReferences
{
    public Transform missionPanel, missionPreviewContentPanel;
}

[Serializable]
public class SpriteDatabase
{
    public Sprite fireDamageType, physicalDamageType, bleedingDamageType;
}

public class UIManager : MonoBehaviour
{
    #region STATIC REFERENCE INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static UIManager i;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (i == null)
            //if not, set instance to this
            i = this;
        //If instance already exists and it's not this:
        else if (i != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
    }

    #endregion

    public PrefabReferences prefabs;
    public UIReferences panels;
    public SpriteDatabase sprites;

    void Start()
    {
        // hide prefab templates
        panels.missionPreviewContentPanel.DeactivateChild();
    }

    //internal static void CreateFloatingTextForUnit(Mission exp, Unit curTarget, int value)
    //{
    //    CreateFloatingText(GetUnitUITarget(exp, curTarget), value);
    //}

    //internal static void CreateEffectAnimation(Mission exp, Unit curTarget, MonoBehaviour prefab)
    //{
    //    var effect = Instantiate(prefab, GetUnitUITarget(exp, curTarget));

    //    // UNDONE: fine until not fine
    //    if (curTarget is Enemy)
    //        MirrorByX(effect);
    //}

    public static void MirrorByX(Transform trans)
    {
        var rect = (RectTransform) trans;
        //TODO: can skip rotation each frame if not locked in animation
        // mirror object position by X axis
        rect.anchoredPosition = new Vector2(-rect.anchoredPosition.x, rect.anchoredPosition.y);
        // mirror object sprite by X axis
        rect.localRotation = Quaternion.Euler(0, 0, 180);
    }

    public static void TriggerAnimators(string triggerMessage, params Animator[] animators)
    {
        animators.ForEach(anim => anim.SetTrigger(triggerMessage));
    }
}