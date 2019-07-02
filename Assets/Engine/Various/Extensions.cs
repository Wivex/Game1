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
    /// Instantiates prefab, ties it to Transform component of parent object, returns component from prefab instance of desired type
    /// </summary>
    public static T Create<T>(this MonoBehaviour prefab, Transform parent)
    {
        var expPanel = Object.Instantiate(prefab, parent);
        return expPanel.GetComponent<T>();
    }
}