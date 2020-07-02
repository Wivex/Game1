﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using GameObject = UnityEngine.GameObject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class Extensions
{
    /// <summary>
    /// Implement list.ForEach(elem => action) for IEnumerable
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var element in source) action(element);
    }

    /// <summary>
    /// Returns first element of collection and removes it from it
    /// </summary>
    public static T ExtractFirstElement<T>(this ICollection<T> source)
    {
        var elem = source.FirstOrDefault();
        source.Remove(elem);
        return elem;
    }

    /// <summary>
    /// Destroys all children objects (clean up prefab templates)
    /// </summary>
    public static Transform DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }

        return transform;
    }

    //TODO: check if needed?
    /// <summary>
    /// Performs SetActive operation for this object and all it's children, which have Comp<T>
    /// </summary>
    public static void ChangeActiveDescending<T>(this GameObject obj, bool state)
    {
        foreach (var comp in obj.GetComponentsInChildren<T>(true))
        {
            (comp as Behaviour).gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// Set enabled property of the Comp<T> for this object and all it's children
    /// </summary>
    public static void ChangeEnabledDescending<T>(this GameObject obj, bool state)
    {
        foreach (var comp in obj.GetComponentsInChildren<T>(true))
        {
            (comp as Behaviour).enabled = state;
        }
    }

    /// <summary>
    /// Changes Visible property of this canvas content (it's Drawer and sub-Drawers)
    /// </summary>
    public static void ChangeContentVisibility(this Canvas canvas, bool visible)
    {
        if (visible)
        {
            canvas.gameObject.IterateEnableNestedUI();
        }
        else
        {
            canvas.gameObject.DisableNestedUI();
        }
    }

    // UNDONE: bad performance of GetComponent predicted, require caching of Comps
    /// <summary>
    /// Recursively iterates direct children of the game object and restores their visibility/functionality
    /// </summary>
    public static void IterateEnableNestedUI(this GameObject obj)
    {
        // if object has CM component, let it control it's children visibility
        var CM = obj.GetComponent<CanvasManager>();
        if (CM != null) CM.ResetCanvases();
        else
        {
            // iterate direct children
            foreach (Transform child in obj.transform)
            {
                // enable Canvas, if exist
                Behaviour comp = child.GetComponent<Canvas>();
                if (comp != null) comp.enabled = true;

                // enable Drawer, if exist
                comp = child.GetComponent<Drawer>();
                if (comp != null) comp.enabled = true;

                // iterate this method on SubChildren (until no children left)
                if (child.transform.childCount > 0)
                {
                    child.gameObject.IterateEnableNestedUI();
                }
            }
        }
    }

    /// <summary>
    /// Disables this and all sub Canvases and Drawers
    /// </summary>
    public static void DisableNestedUI(this GameObject obj)
    {
        obj.ChangeEnabledDescending<Canvas>(false);
        obj.ChangeEnabledDescending<Drawer>(false);
    }

    public static IEnumerable<Canvas> DirectSubCanvases(this GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            var canvas = child.GetComponent<Canvas>();
            if (canvas != null)
                yield return canvas;
        }
    }

    /// <summary>
    /// Returns one random value by weighted chance from list
    /// </summary>
    public static int GetOneByWeight(this List<ChanceWeight> list)
    {
        var totalWeight = list.Sum(elem => elem.chanceWeight);
        var roll = Random.Range(1, totalWeight);
        for (var i = 0; i < list.Count; i++)
        {
            roll -= list[i].chanceWeight;
            if (roll <= 0) return i;
        }

        return -1;
    }

    /// <summary>
    /// Returns one random value by weighted chance from list
    /// </summary>
    public static List<float> ToProbabilityList(this List<ChanceWeight> weightList)
    {
        var probabilityList = new List<float>(weightList.Capacity);
        var totalWeight = weightList.Sum(elem => elem.chanceWeight);
        foreach (var elem in weightList)
        {
            probabilityList.Add((float) elem.chanceWeight / totalWeight);
        }

        return probabilityList;
    }
}