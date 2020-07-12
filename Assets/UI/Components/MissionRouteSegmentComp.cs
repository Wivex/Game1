using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class MissionRouteSegment
{
    public MissionRouteSegmentComp toggleComp;
    [Tooltip("Represent number of sites required to traverse in this zone in order to reach connected zone")]
    public int pathLength;
}


public class MissionRouteSegmentComp : MonoBehaviour
{
    public ZoneData zone;

    [Reorderable(ReorderableNamingType.ReferencedObjectName, "toggleComp")]
    public List<MissionRouteSegment> connectedSegments;

    internal Toggle toggle; 

    TextMeshProUGUI label;

    void OnEnable()
    {
        toggle = GetComponentInChildren<Toggle>();
        label = GetComponentInChildren<TextMeshProUGUI>();

        label.text = zone.name; 
    }
}