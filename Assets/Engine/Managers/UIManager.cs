using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    
    #region GLOBAL UI PREFABS REFERENCES

    public static MonoBehaviour floatingTextPrefab, meleeHitEffectPrefab, missionOverviewPanelPrefab;

    #endregion
    
    #region GLOBAL UI PANELS REFERENCES
    
    public Transform missionPanel, missionPreviewContentPanel;

    #endregion


    //internal static void CreateFloatingTextForUnit(Mission exp, Unit target, int value)
    //{
    //    CreateFloatingText(GetUnitUITarget(exp, target), value);
    //}

    //internal static void CreateEffectAnimation(Mission exp, Unit target, MonoBehaviour prefab)
    //{
    //    var effect = Instantiate(prefab, GetUnitUITarget(exp, target));

    //    // UNDONE: fine until not fine
    //    if (target is Enemy)
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
        var floatingText = Instantiate(floatingTextPrefab, target);
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