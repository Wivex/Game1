using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class Extensions
{
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
    /// performs SetActive operation for all child objects of type T for this object
    /// </summary>
    public static void SetActiveForChildren<T>(this MonoBehaviour obj, bool state)
    {
        foreach (var child in obj.GetComponentsInChildren<T>(true))
        {
            (child as MonoBehaviour).gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// performs SetActive operation for all child objects of type T for this object
    /// </summary>
    public static void SetActiveForChildren<T>(this Transform obj, bool state)
    {
        foreach (var child in obj.GetComponentsInChildren<T>(true))
        {
            (child as MonoBehaviour).gameObject.SetActive(state);
        }
    }
}