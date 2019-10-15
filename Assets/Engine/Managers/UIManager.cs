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

    public static int f = 0;

    internal MonoBehaviour floatingTextPrefab, meleeHitPrefab;

    Transform GetUnitUITarget(Expedition exp, Unit target) => target is Hero
        ? expPanelDrawManager.expPreviewPanels[exp].heroIcon.transform
        : expPanelDrawManager.expPreviewPanels[exp].objectIcon.transform;

    // initializing prefabs on Unity editor refresh
    void OnEnable()
    {
        floatingTextPrefab = Resources.Load<MonoBehaviour>("Effects/FloatingText/FloatingText");
        meleeHitPrefab = Resources.Load<MonoBehaviour>("Effects/MeleeHit/MeleeHit");
    }

    internal void CreateFloatingTextForUnit(Expedition exp, Unit target, int value)
    {
        CreateFloatingText(GetUnitUITarget(exp, target), value);
    }

    internal void CreateEffectAnimation(Expedition exp, Unit target, MonoBehaviour prefab)
    {
        var effect = Instantiate(prefab, GetUnitUITarget(exp, target));

        // UNDONE: fine until not fine
        if (target is Enemy)
            MirrorEffectAxisX(effect);
    }

    void MirrorEffectAxisX(MonoBehaviour obj)
    {
        // invert x and y offsets
        obj.transform.localPosition =
            new Vector3(-obj.transform.localPosition.x, -obj.transform.localPosition.y, obj.transform.localPosition.z);

        // apply required rotation
        obj.transform.eulerAngles += new Vector3(0, 180, 0);
    }

    void CreateFloatingText(Transform target, int value)
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