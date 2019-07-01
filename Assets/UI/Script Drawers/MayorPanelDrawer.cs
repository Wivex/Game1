using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MayorPanelDrawer : MonoBehaviour
{
    TextMeshProUGUI noQuestsText, noExpText;

    // initializations
    void Awake()
    {
        UIManager.DestroyChildren(this);
    }
}