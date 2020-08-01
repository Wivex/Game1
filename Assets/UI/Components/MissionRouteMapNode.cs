using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class MissionRouteMapNode : MonoBehaviour
{
    public ZoneData zone;
    public List<MissionRouteSegment> connections;

    internal Toggle toggle; 

    TextMeshProUGUI label;

    void Awake()
    {
        toggle = GetComponentInChildren<Toggle>();
        label = GetComponentInChildren<TextMeshProUGUI>();

        label.text = zone.name; 
    }
}