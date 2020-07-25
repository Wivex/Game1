using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using TMPro;
using UnityEngine.UI;


public class MissionRouteMapNode : MonoBehaviour
{
    public ZoneData zone;
    [Reorderable(ReorderableNamingType.ReferencedObjectName, "zone")]
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