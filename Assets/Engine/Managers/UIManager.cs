using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

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

    public ExpeditionPanelDrawManager expPanelDrawManager;
    public MayorPanelDrawer mayorPanelDrawer;
    public TavernPanelDrawer tavernPanelDrawer;

    public FloatingText floatingTextPrefab;

    public void CreateFloatingDamageText(Expedition exp, Unit target, int value)
    {
        var UItarget = target is Hero
            ? expPanelDrawManager.expPreviewPanels[exp].heroIcon.transform
            : expPanelDrawManager.expPreviewPanels[exp].objectIcon.transform;

        CreateFloatingText(UItarget, -value);
    }

    public void CreateFloatingText(Transform target, int value)
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