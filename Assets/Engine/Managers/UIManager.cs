using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class PrefabsReferences
{
    public GameObject floatingTextPrefab, meleeHitEffectPrefab, missionOverviewPanelPrefab;
}

[Serializable]
public class UIReferences
{
    public Transform missionPanel, missionPreviewContentPanel;
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

    public PrefabsReferences prefabs;
    public UIReferences panels;

    //internal static void CreateFloatingTextForUnit(Mission exp, Unit curTarget, int value)
    //{
    //    CreateFloatingText(GetUnitUITarget(exp, curTarget), value);
    //}

    //internal static void CreateEffectAnimation(Mission exp, Unit curTarget, MonoBehaviour prefab)
    //{
    //    var effect = Instantiate(prefab, GetUnitUITarget(exp, curTarget));

    //    // UNDONE: fine until not fine
    //    if (curTarget is Enemy)
    //        MirrorEffectAxisX(effect);
    //}

    static void MirrorEffectAxisX(MonoBehaviour obj)
    {
        // invert x and y offsets
        obj.transform.localPosition =
            new Vector3(-obj.transform.localPosition.x, -obj.transform.localPosition.y, obj.transform.localPosition.z);

        // apply required rotation
        obj.transform.eulerAngles += new Vector3(0, 180, 0);
    }

    static void CreateFloatingText(Transform target, int value)
    {
        var floatingText = new MonoBehaviour();
        // var floatingText = Instantiate(floatingTextPrefab, curTarget);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();

        if (value > 0)
        {
            textObject.text = $"+{value}";
            textObject.color = Color.green;
        }

        if (value < 0)
        {
            textObject.text = $"{value}";
            textObject.color = Color.red;
        }
    }
}