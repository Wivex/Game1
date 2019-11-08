using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

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

    /// <summary>
    /// Performs SetActive operation for all child objects of type T for this object
    /// </summary>
    public static void SetActiveForChildren<T>(this MonoBehaviour obj, bool state)
    {
        foreach (var child in obj.GetComponentsInChildren<T>(true))
        {
            (child as MonoBehaviour).gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// Performs SetActive operation for all child objects of type T for this object
    /// </summary>
    public static void SetActiveForChildren<T>(this Transform obj, bool state)
    {
        foreach (var child in obj.GetComponentsInChildren<T>(true))
        {
            (child as MonoBehaviour).gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// Changes Visible property of this canvas content (it's Drawer and sub-Drawers)
    /// </summary>
    public static void ChangeContentVisibility(this Canvas canvas, bool visibility)
    {
        // change visibility for all sub-canvases
        var subDrawers = canvas.GetComponentsInChildren<ICanvasVisibility>().ToList();
        subDrawers.ForEach(drawer => drawer.Visible = visibility);
    }

    public static List<Canvas> DirectSubCanvases(this GameObject obj) =>
        obj.GetComponentsInChildren<Canvas>()
           .Where(canvas => canvas.transform.parent == obj.transform).ToList();
}