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

    /// <summary>
    /// Mirror object position by X axis
    /// </summary>
    /// <param name="lockedRotation">Override rotation each frame it's locked by animation</param>
    public static void MirrorByX(Transform trans, bool lockedRotation = false)
    {
        if (lockedRotation) trans.localEulerAngles += new Vector3(0, 180, 0);
        var rect = (RectTransform) trans;
        rect.anchoredPosition = new Vector2(-rect.anchoredPosition.x, rect.anchoredPosition.y);
    }

    public static void TriggerAnimators(string triggerMessage, params Animator[] animators)
    {
        animators.ForEach(anim => anim.SetTrigger(triggerMessage));
    }
}