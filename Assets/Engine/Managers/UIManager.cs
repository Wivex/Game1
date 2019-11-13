using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    internal static ExpeditionPanelManager expPanelManager;
    internal static MayorPanelDrawer mayorPanelDrawer;
    internal static TavernPanelDrawer tavernPanelDrawer;
    internal static MonoBehaviour floatingTextPrefab, meleeHitEffectPrefab;

    static Transform GetUnitUITarget(Expedition exp, Unit target) => target is Hero
        ? expPanelManager.expPreviewPanels[exp].heroIcon.transform
        : expPanelManager.expPreviewPanels[exp].objectIcon.transform;

    // initializing prefabs on Unity editor refresh
    void OnEnable()
    {
        floatingTextPrefab = Resources.Load<MonoBehaviour>("Effects/FloatingText/FloatingText");
        meleeHitEffectPrefab = Resources.Load<MonoBehaviour>("Effects/MeleeHit/MeleeHit");

        expPanelManager = GameObject.Find("Expedition Panel").GetComponent<ExpeditionPanelManager>();
        mayorPanelDrawer = GameObject.Find("Mayor Panel").GetComponent<MayorPanelDrawer>();
        tavernPanelDrawer = GameObject.Find("Tavern Panel").GetComponent<TavernPanelDrawer>();
    }

    internal static void CreateFloatingTextForUnit(Expedition exp, Unit target, int value)
    {
        CreateFloatingText(GetUnitUITarget(exp, target), value);
    }

    internal static void CreateEffectAnimation(Expedition exp, Unit target, MonoBehaviour prefab)
    {
        var effect = Instantiate(prefab, GetUnitUITarget(exp, target));

        // UNDONE: fine until not fine
        if (target is Enemy)
            MirrorEffectAxisX(effect);
    }

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