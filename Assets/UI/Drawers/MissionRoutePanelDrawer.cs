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
            comp.toggle.onValueChanged.AddListener(arg0 => UpdateToggles(comp));
    }

    void UpdateToggles(MissionRouteSegmentComp changedSeg)
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

    void UpdateSegmentsInteractivity(MissionRouteSegmentComp changedSeg)
    {
        if (changedSeg.toggle.isOn)
        {
            // disable all
            routeSegments.ForEach(seg => seg.toggle.interactable = false);
            foreach (var seg in changedSeg.connectedSegments)
            {
                // enable connected
                seg.toggleComp.toggle.interactable = true;
                // except previous in chain, if it exists
                if (route.Any() &&
                    route.Last().Key == seg.toggleComp.zone)
                {
                    seg.toggleComp.toggle.interactable = false;
                }
            }
            // enable self for untoggle possibility
            changedSeg.toggle.interactable = true;
        }
        else
        {
            if (route.Any())
            {
                // disable all
                routeSegments.ForEach(seg => seg.toggle.interactable = false);
                var previousSeg = changedSeg.connectedSegments.Find(seg => seg.toggleComp.zone == route.Last().Key);
                // enable all connected for previous in chain
                previousSeg.toggleComp.connectedSegments.ForEach(seg => seg.toggleComp.toggle.interactable = true);
                // except pre-previous in chain, if it exists
                var prepreviousZone = route.ElementAtOrDefault(route.Count - 2).Key;
                if (prepreviousZone != null)
                {
                    previousSeg.toggleComp.connectedSegments.Find(seg => seg.toggleComp.zone == prepreviousZone).toggleComp.toggle.interactable = false;
                }
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
        startingSegments.ForEach(seg => seg.toggle.interactable = true);
    }
}