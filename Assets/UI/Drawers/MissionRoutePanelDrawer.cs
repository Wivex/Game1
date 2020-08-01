using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MissionRoutePanelDrawer : Drawer
{
    public List<MissionRouteMapNode> startingNodes;

    List<MissionRouteMapNode> allNodes;
    List<MissionRouteSegment> setUpPath;

    int PathLength(MissionRouteMapNode changedNode) =>
        setUpPath.Any()
            ? changedNode.connections.Find(node => setUpPath.Last().zone == node.zone).length
            : 0;


    void Start()
    {
        allNodes = GetComponentsInChildren<MissionRouteMapNode>().ToList();
        setUpPath = MissionsManager.missionSetUp.path;
        // add listener to each toggle to take action when any toggle state changes
        foreach (var comp in allNodes)
            comp.toggle.onValueChanged.AddListener(arg0 => OnNodeToggle(comp));
    }

    void OnNodeToggle(MissionRouteMapNode changedNode)
    {
        if (changedNode.toggle.isOn)
        {
            setUpPath.Add(new MissionRouteSegment(changedNode.zone, PathLength(changedNode)));
        }
        else
        {
            setUpPath.Remove(setUpPath.Last());
        }

        UpdateNodesInteractivity(changedNode);
    }

    void UpdateNodesInteractivity(MissionRouteMapNode changedNode)
    {
        // toggle has been enabled
        if (changedNode.toggle.isOn)
        {
            // disable all, except connected nodes, which are not already in route
            allNodes.ForEach(node => node.toggle.interactable =
                changedNode.connections.Exists(seg => seg.zone == node.zone) &&
                !setUpPath.Exists(seg => seg.zone == node.zone));

            // enable self for untoggle possibility
            changedNode.toggle.interactable = true;
        }
        else
        {
            if (setUpPath.NotNullOrEmpty())
            {
                var lastRouteNode = allNodes.Find(node => setUpPath.Last().zone == node.zone);
                // disable all, except connected for last node, which are not already in route
                allNodes.ForEach(node => node.toggle.interactable =
                    lastRouteNode.connections.Exists(seg => seg.zone == node.zone) &&
                    !setUpPath.Exists(seg => seg.zone == node.zone));

                // enable self for untoggle possibility
                lastRouteNode.toggle.interactable = true;
            }
            else
            {
                // disable all but starting Nodes
                allNodes.ForEach(node => node.toggle.interactable = false);
                startingNodes.ForEach(node => node.toggle.interactable = true);
            }
        }
    }

    public void InitPanel()
    {
        setUpPath.Clear();

        allNodes.ForEach(seg => seg.toggle.interactable = false);
        allNodes.ForEach(seg => seg.toggle.isOn = false);
        startingNodes.ForEach(seg => seg.toggle.interactable = true);
    }
}