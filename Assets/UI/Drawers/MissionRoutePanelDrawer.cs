using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionRoutePanelDrawer : Drawer
{
    [Reorderable(ReorderableNamingType.ReferencedObjectName)]
    public List<MissionRouteSegmentComp> startingSegments;

    List<MissionRouteSegmentComp> routeSegments;
    /// <summary>
    /// {MissionsManager.missionSetUp.route} shortcut
    /// </summary>
    Dictionary<ZoneData, int> route;


    int PathToSegmentLength(MissionRouteSegmentComp targetSegment) =>
        route.Any()
            ? targetSegment.connectedSegments.Find(seg =>
                route.Last().Key).pathLength
            : 0;


    void Start()
    {
        route = MissionsManager.missionSetUp.route;
        routeSegments = GetComponentsInChildren<MissionRouteSegmentComp>().ToList();
        // add listener to each toggle to take action when any toggle state changes
        foreach (var comp in routeSegments)
            comp.toggle.onValueChanged.AddListener(arg0 => OnSegmentToggle(comp));
}

    void OnSegmentToggle(MissionRouteSegmentComp changedSeg)
    {
        if (changedSeg.toggle.isOn)
        {
            route.Add(changedSeg.zone, PathToSegmentLength(changedSeg));
        }
        else
        {
            // TODO: check for last (first) elem
            route.Remove(changedSeg.zone);
        }
        UpdateSegmentsInteractivity(changedSeg);
    }

    void UpdateSegmentsInteractivity(MissionRouteSegmentComp changedSegComp)
    {
        // toggle has been enabled
        if (changedSegComp.toggle.isOn)
        {
            // disable all
            routeSegments.ForEach(seg => seg.toggle.interactable = false);
            foreach (var seg in changedSegComp.connectedSegments)
            {
                // enable connected segments, if not already in route
                if (!route.ContainsKey(seg.toggleComp.zone))
                    seg.toggleComp.toggle.interactable = true;
            }
            // enable self for untoggle possibility
            changedSegComp.toggle.interactable = true;
        }
        else
        {
            if (route.Any())
            {
                // disable all
                routeSegments.ForEach(seg => seg.toggle.interactable = false);
                // find current last segment in route
                var lastSegComp = changedSegComp.connectedSegments.Find(seg => seg.toggleComp.zone == route.Last().Key).toggleComp;
                foreach (var seg in lastSegComp.connectedSegments)
                {
                    // enable connected segments, if not already in route
                    if (!route.ContainsKey(seg.toggleComp.zone))
                        seg.toggleComp.toggle.interactable = true;
                }
                // enable self for untoggle possibility
                lastSegComp.toggle.interactable = true;
            }
            else
            {
                // disable all but starting segments
                routeSegments.ForEach(seg => seg.toggle.interactable = false);
                startingSegments.ForEach(seg => seg.toggle.interactable = true);
            }
        }
    }

    public void InitPanel()
    {
        route.Clear();
        
        routeSegments.ForEach(seg => seg.toggle.interactable = false);
        routeSegments.ForEach(seg => seg.toggle.isOn = false);
        startingSegments.ForEach(seg => seg.toggle.interactable = true);
    }
}